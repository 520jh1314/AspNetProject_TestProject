using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;

namespace QProject.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 添加控制台日志格式化器
        services.AddConsoleFormatter();
        // 添加 JWT 验证
        services.AddJwt<JwtHandler>();
        // 添加跨域访问
        services.AddCorsAccessor();
        // 添加虚拟文件服务
        services.AddVirtualFileServer();
        // 添加控制器并注入 UnifyResult
        services.AddControllers()
                .AddInjectWithUnifyResult();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseInject(string.Empty);

        #region 文件服务
        app.UseStaticFiles();
        var staticRoot = Path.Combine(AppContext.BaseDirectory, "uploads");
        if (!Directory.Exists(staticRoot)) Directory.CreateDirectory(staticRoot);
        //配置文件服务中间件
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(staticRoot),
            RequestPath = "/uploads",
            //禁用目录浏览
            //EnableDirectoryBrowsing = true
        });
        #endregion

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
