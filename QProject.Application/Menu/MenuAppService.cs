using Azure;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DataEncryption.Extensions;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QProject.Application.Menu.Dtos;
using QProject.Application.Tool;
using QProject.Core;
using System;
using System.Collections.Generic;
using System.Collections.Generic.Enumerable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using QProject.Application.Tool;

namespace QProject.Application.Menu
{

    [ApiDescriptionSettings(true, Name = "菜单相关", Groups = new string[] { "规范化接口演示", "菜单服务" }, Tag = "含查询、添加、父子关系的递归取值、分页、加解密、数据格式化、" +
        "uuid随机生成")]
    [Route("menu")]
    public class MenuAppService : IDynamicApiController
    {
        public readonly IRepository<ProjectFileMean> _projectfilemeanIRepository;

        public readonly IRepository<ProjectFileInfo> _projectfileInfoIRepository;

        public MenuAppService(IRepository<ProjectFileMean> projectfilemeanIRepository,
            IRepository<ProjectFileInfo> projectfileInfo
            )
        {
            _projectfilemeanIRepository = projectfilemeanIRepository;
            _projectfileInfoIRepository = projectfileInfo;

        }
        
        /// <summary>
        /// 获取N级菜单通过递归 ——父子关系
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ProjectMenuContentDto> getinterjectFileMeanings()
        {
            var projectfilemeanIRepository = _projectfilemeanIRepository.AsQueryable();
            _ = projectfilemeanIRepository.First() ?? throw Oops.Oh("暂无");
            var menu = projectfilemeanIRepository
                .Where(a => a.MenuLevel == 1)
                .OrderBy(a => a.ListMenuCode)
                .Select(a => new ProjectMenuContentDto
                {
                    FCID = a.FCID,
                    FatherNode = a.FatherNode,
                    ListName = a.ListName,
                    ListMenuCode = a.ListMenuCode,
                    MenuLevel = a.MenuLevel,
                    SecondLevelChildMenu = projectfilemeanIRepository
                    .Where(u => u.FatherNode == a.FCID)
                    .OrderBy(u => u.ListMenuCode)
                    .Select(u => new ProjectMenuContentDto
                    {
                        FCID = u.FCID,
                        ListName = u.ListName,
                        ListMenuCode = u.ListMenuCode,
                        ThreeLevelChildMenu = projectfilemeanIRepository
                        .Where(c => c.MenuLevel == 3 && c.FatherNode == u.FCID)
                        .OrderBy(c => c.ListMenuCode)
                        .Select(c => new ProjectMenuContentDto
                        {
                            FCID = c.FCID,
                            ListName = c.ListName,
                            ListMenuCode = c.ListMenuCode
                        }).ToList()
                    }).ToList()
                }).AsQueryable().ToList();
            return menu;
        }

        /// <summary>
        /// 获取菜单全部内容 ——分页、所有参数作为一个对象
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<PagedList<ProjectMenuContentDto>> gettotalcontentmenu([DefaultValue(1)] int pageindex, [DefaultValue(10)] int pagelist, ProjectMenuContentDtoObject projectmenucontentobject)
        {
            var projectfilemeanIRepository = _projectfilemeanIRepository.AsQueryable();
            var totalcontentmenu = await projectfilemeanIRepository
                .Where(!string.IsNullOrEmpty(projectmenucontentobject.MenuLevel), a => a.MenuLevel.ToString().Contains(projectmenucontentobject.MenuLevel))
                .Where(!string.IsNullOrEmpty(projectmenucontentobject.ListMenuCode), a => a.ListMenuCode.Contains(projectmenucontentobject.ListMenuCode))
                .Where(!string.IsNullOrEmpty(projectmenucontentobject.ListName), a => a.ListName.Contains(projectmenucontentobject.ListName))
                .Select(a => new ProjectMenuContentDto
                {
                    FCID = a.FCID,
                    FatherNode = a.FatherNode,
                    ListName = a.ListName,
                    ListMenuCode = a.ListMenuCode,
                    MenuLevel = a.MenuLevel
                }).ToPagedListAsync(pageindex, pagelist);

            return totalcontentmenu;
        }

        /// <summary>
        /// 获取菜单信息 ——查询、一个一个的参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedList<ProjectFileInfoDtos>> gettotalcontentmenucrossdatabase([DefaultValue(1)] int pageindex, [DefaultValue(10)] int pagelist, string fcid)
        {
            var projectfileInfo = _projectfileInfoIRepository.AsQueryable();

            var info = await projectfileInfo.Where(a => a.FCID == fcid).Select(a => new ProjectFileInfoDtos
            {
                FIID = a.FIID,
                FCID = a.FCID,
                FlieName = a.FlieName,
                UploadName = a.UploadName

            }).ToPagedListAsync();

            return info;
        }


        /// <summary>
        /// 添加具体菜单信息 ——含添加、加密（DESC）、随机生成uuid
        /// </summary>
        /// <param name="projectfileInfodtos"></param>
        /// <param name="fcid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> addProjectFileInfo(addProjectFileInfoDtos projectfileInfodtos,[DefaultValue("0334d70d-9141-493d-acbf-860f995f63fe")] string fcid)
        {
            //随即生成uuid
            var uuid = Guid.NewGuid().ToString(); // 9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
            var uuidN = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09
            var uuidD = Guid.NewGuid().ToString("D"); // 9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
            var uuidB = Guid.NewGuid().ToString("B"); // {734fd453-a4f8-4c5d-9c98-3fe2d7079760}
            var uuidP = Guid.NewGuid().ToString("P"); // (ade24d16-db0f-40af-8794-1e08e2040df3)
            var uuidX = Guid.NewGuid().ToString("X"); // {0x3fa412e3,0x8356,0x428f,{0xaa,0x34,0xb7,0x40,0xda,0xaf,0x45,0x6f}}

            //desc加密
            var uploadName =  DESCEncryption.Encrypt(projectfileInfodtos.UploadName, "UploadName");

            var IdCardInform = DESCEncryption.Encrypt(projectfileInfodtos.IdCardInform, "IdCard");

            var info = projectfileInfodtos.Adapt<ProjectFileInfo>();
            await _projectfileInfoIRepository.InsertAsync(new ProjectFileInfo { 
                FIID = uuid,
                FCID = fcid,
                FlieName = info.FlieName,
                UploadName = uploadName,
                Unit = info.Unit,
                IdCardInform = IdCardInform
            });

            return "添加成功";
            
        }


        /// <summary>
        /// 获取项目文件信息 ——解密查询、分页 、数据格式化（身份证信息格式化中间用*代替）
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagelist"></param>
        /// <param name="fcid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedList<ProjectFileInfoDtos>> getprojectfileInfo([DefaultValue(1)]int pageindex, [DefaultValue(10)]int pagelist, [DefaultValue("0334d70d-9141-493d-acbf-860f995f63fe")]string fcid)
        {
            var projectfileInfo =  _projectfileInfoIRepository.AsQueryable();
            DataFormatTools dataFormat = new DataFormatTools();

            var info = await projectfileInfo
                .Where(a => a.FCID == fcid)
                .Select(a => new ProjectFileInfoDtos
                {
                FIID = a.FIID,
                FlieName = a.FlieName,
                UploadName = a.UploadName != null ? DESCEncryption.Decrypt(a.UploadName.ToString(), "UploadName", false) : null,
                Unit = a.Unit,
                IdCardInform = a.IdCardInform != null ? DataFormatTools.ReplaceWithSpecialChar(DESCEncryption.Decrypt(a.IdCardInform.ToString(), "IdCard", false).ToString(),5,5,'*') : null
                }).AsQueryable().ToPagedListAsync(pageindex,pagelist);
            
            return info;
        }




    }
}
