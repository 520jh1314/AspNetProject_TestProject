using Azure;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using QProject.Application.Documentation.Dtos;
using QProject.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QProject.Application.Documentation
{
    [ApiDescriptionSettings(true, Tag = "文件上传", Groups = new string[] { "文件服务" })]
    public class DocumentationAppService : IDynamicApiController
    {

        private addDocumentationDto DocumentationDto = null;
        //文件物理目录
        private static readonly string FileUrl = "wwwroot/uploads/document/";

        #region FileStore
        private readonly IRepository<FileStore> _filestoreIRepository;
        public DocumentationAppService(IRepository<FileStore> filestoreIRepository)
        {
            _filestoreIRepository = filestoreIRepository;
        }
        #endregion


        /// <summary>
        /// 含单文件上传、 多文件上传 、数据存储到数据库（物理路径）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        ///[FromForm]  （选择参数）
        [HttpPost]
        public async Task UploadFileLists(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                await UploadFileAsync(file);
            }

        }

        /// <summary>
        /// 下载文件(物理路径)
        /// </summary>
        /// <param name="fcid"></param>
        /// <returns></returns>
        [HttpGet, NonUnify]
        public IActionResult getfile([DefaultValue("778cf3f1e2a7479b99b52871df39a46e")] string fcid)
        {
            var fileStore = _filestoreIRepository.AsQueryable().Single(a => a.FCID == fcid);

            Console.Write(fileStore.FilePath);

            return new FileStreamResult(new FileStream(fileStore.FilePath, FileMode.Open), "application/octet-stream")
            {
                FileDownloadName = fileStore.Name + fileStore.SuffixName,

            };
        }

        /// <summary>
        /// 文件预览
        /// </summary>
        /// <param name="fcid"></param>
        /// <returns></returns>
        public async Task<string> getviewfile(string fcid)
        {
            var file =await  _filestoreIRepository.AsQueryable().FirstOrDefaultAsync(a => a.FCID == fcid);
            _ = file ?? throw Oops.Oh("无该数据");

            return $"{file.FilePath}";
        }

        /// <summary>
        /// 多文件软删除
        /// </summary>
        /// <param name="fcids"></param>
        /// <returns></returns>
        public async Task<string> deletesfiles(List<string> fcids)
        {
            foreach (var fcid in fcids) {
                await deletefile(fcid);
            }

            return "ok";
        }




        #region 方法
        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<List<string>> UploadFileAsync(IFormFile file)
        {

            string Nowdate = FileUrl + DateTime.Now.ToString("d");  //日期格式：2111/01/01

            //主(获取路径)
            string savePath = Path.Combine(Nowdate);
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);


            int size = (int)(file.Length / 1024.0);  // 文件转换为kb格式 （自选是否添加）
            int ifbig = (int)1024.0 * 1;   //将1mb的kb值
            _ = size >= ifbig ? throw Oops.Oh(file.FileName + "大于或等于1MB") : "成功";   //限制文件大小

            #region 文件存储位置filePath
            // var clientFileName = file.FileName; // 客户端上传的文件名带后缀名  （自选是否添加）
            string contentType = file.ContentType; // 获取文件 ContentType 或解析 MIME 类型   （自选是否添加）
            string datetime = DateTime.Now.ToString("F");
            var FileNameNoSuffix = Path.GetFileNameWithoutExtension(file.FileName);
            //主（设置文件名）
            var fileName = FileNameNoSuffix + datetime + Path.GetExtension(file.FileName);
            //主（将上方获取的路径、文件名组合成路径）
            var filePath = Path.Combine(savePath, fileName).Replace(" ", "_").Replace(":", "-");
            #endregion

            // 主（保存到指定路径）
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            #region  新增（存文件路径）
            var FileStore = DocumentationDto.Adapt<FileStore>();
            await _filestoreIRepository.InsertAsync(new FileStore
            {
                FCID = Guid.NewGuid().ToString("N"),
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                SuffixName = Path.GetExtension(file.FileName),
                FilePath = filePath,
                IsDeleted = 1
            });;
            #endregion


            #region
            // 返回文件名（这里可以自由返回更多信息）
            return new List<string>  {
                filePath,       //文件路径
                fileName,       //文件名称
                datetime,       //上传时间
                contentType,        //文件类型
                size.ToString(),   //文件大小
                ifbig.ToString(),   //定义限制
                "添加成功"
            };
            #endregion
        }



        /// <summary>
        /// 软删除文件
        /// </summary>
        /// <param name="fcid"></param>
        /// <returns></returns>
        private async Task deletefile(string fcid)
        {
            var firstcener = await _filestoreIRepository.FirstOrDefaultAsync(a => a.FCID == fcid);
            _ = firstcener ?? throw Oops.Oh("暂无该文件");
            firstcener.IsDeleted = 0;
            await _filestoreIRepository.UpdateAsync(firstcener);
        }
        #endregion





    }
}
