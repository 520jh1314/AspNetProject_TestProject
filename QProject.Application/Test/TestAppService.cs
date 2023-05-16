using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using QProject.Application.test.Dtos;
using QProject.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QProject.Application.test
{
    [ApiDescriptionSettings(Groups = new string[] { "规范化接口演示" }, Name = "增删改查示例")]
    [Route("Test")]
    public class TestAppService : IDynamicApiController
    {
        private readonly IRepository<User> _userIRepository;

        private readonly IRepository<Blog> _blogIRepository;

        private readonly IRepository<UserBlog> _userblogIRepository;

        public TestAppService(IRepository<User> userIRepository,
        IRepository<Blog> blogIRepository,
        IRepository<UserBlog> userblogIRepository
        )
        {
            _userIRepository = userIRepository;
            _blogIRepository = blogIRepository;
            _userblogIRepository = userblogIRepository;
        }


        #region 打招呼
        /// <summary>
        /// 打招呼
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string SayHello(string name)
        {
            return $"hello sss {name}";
        }
        #endregion

        /// <summary>
        /// 初始化字符串数组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<string> getpersonname()
        {
            return new List<string>
            {
                "小红",
                "小白"
            };
        }
        /// <summary>
        /// 初始化字符串数组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string[] getStringlist()
        {
            return new string[] { "as", "asd" };
        }

        /// <summary>
        /// 初始化对象数组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Person> getpersoninfo()
        {
            return new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "小白白"
                }
            };
        }


        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UserInput> InsertUserInfo(UserInput input)
        {
            var user = input.Adapt<User>();
            var entry = await _userIRepository.InsertAsync(user);
            return entry.Adapt<UserInput>();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<string> delieteUserInfo(int Id)
        {
            var user = await _userIRepository.FindOrDefaultAsync(Id);
            _ = user ?? throw Oops.Oh("暂无该用户");

            await _userIRepository.DeleteNowAsync(Id);

            return "删除成功";
        }



        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task updateuser(UserInputId input)
        {
            var user = input.Adapt<User>();
            await _userIRepository.UpdateNowAsync(user, ignoreNullValues: true);
        }


        /// <summary>
        /// 查询用户所有信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserInput>> getuserinfo()
        {
            var user = await _userIRepository.AsQueryable().ToListAsync();

            var users = user.Adapt<List<UserInput>>();
            return users;
        }



        /// <summary>
        /// 新增博客
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog> InsertBolgInfo(BlogInput input)
        {
            //实体映射为blog对象  
            //目的：将一个数据从一个持久化存储（例数据库）中提取出来并将其转换为可供应用程序使用的对象
            var blog = input.Adapt<Blog>();

            //动态切换实体仓储  方法一
            //var entry = await _userIRepository.Change<Blog>().InsertAsync(blog);
            //泛型实体仓储 添加  方法二
            var entry = await _blogIRepository.InsertAsync(blog);

            return entry.Entity;
        }

        /// <summary>
        /// 查询用户博客信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<BlogOutInput>> getblogs([Required, Range(1, int.MaxValue)] int userId)
        {
            //检查用户是否存在
            var user = await _userIRepository.FindOrDefaultAsync(userId);
            _ = user ?? throw Oops.Oh("用户不存在");

            var blogs = await _blogIRepository
                .Include(a => a.UserBlog)
                .Include(a => a.UserBlog)
                .OrderByDescending(a => a.CreatedTime)
                .Where(a => a.CreateUserId == userId)
                .Select(a => new BlogOutInput
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    CreateUserId = a.CreateUserId,
                    UserName = a.CreateUser.Name,
                    IsStar = a.UserBlog.Any(U => U.UserId == userId && U.BlogId == a.Id)


                })
                .ToListAsync();
            return blogs;
        }


        /// <summary>
        /// 自定义某用户关注某博客
        /// </summary>
        /// <param name="Star"></param>
        /// <returns></returns>
        public async Task<UserBlog> addStart(BlogStar Star)
        {
            var user = await _userIRepository.FindOrDefaultAsync(Star.userId);
            _ = user ?? throw Oops.Oh("用户不存在");


            var blog = await _blogIRepository.FindOrDefaultAsync(Star.blogId);
            _ = user ?? throw Oops.Oh("博客不存在");

            var Stars = Star.Adapt<UserBlog>();
            var sql = await _userblogIRepository.InsertAsync(new UserBlog
            {
                UserId = Star.userId,
                BlogId = Star.blogId,
                StarStarte = Star.StarStarte
            });
            return sql.Adapt<UserBlog>();

        }

        /// <summary>
        /// 用户关注某博客
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public async Task addBlogStart([Required, Range(1, int.MaxValue)] int userId, [Required, Range(1, int.MaxValue)] int blogId)
        {
            var user = await _userIRepository.FindOrDefaultAsync(userId);
            _ = user ?? throw Oops.Oh("暂无该用户");

            var blog = await _blogIRepository.FindOrDefaultAsync(blogId);
            _ = blog ?? throw Oops.Oh("暂无指定博客");

             await _userblogIRepository.InsertAsync(new UserBlog
            {
                UserId = userId,
                BlogId = blogId
            });


        }


        /// <summary>
        /// 删除用户的关注博客
        /// </summary>
        /// <param name="Id"></param>
        public async Task<int> deleteStar([Required, Range(0, int.MaxValue)] int Id)
        {
            var Star = await _userblogIRepository.FindOrDefaultAsync(Id);
            _ = Star ?? throw Oops.Oh("暂无关注博客");


            //根据EF约定，自动将参数名称映射给数据表中主键列，因此不管参数id改成什么名称，都删除的表UserBlog的主键
            var Firstdelete = await _userblogIRepository.FindAsync(Id);

            await _userblogIRepository.DeleteAsync(Firstdelete);

            return 1;

        }
    }
}
