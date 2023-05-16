using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace QProject.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "QProject.Database.Migrations");
    }
}
