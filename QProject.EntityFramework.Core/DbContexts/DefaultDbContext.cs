using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace QProject.EntityFramework.Core;

[AppDbContext("DbConnectionsqlserver", DbProvider.SqlServer)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}