using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TpwManage.Infrastructure.Persistence.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
  public MyContext CreateDbContext(string[] args)
  {
    string conn = DbConfig.ConnectionString;
    var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
    optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));

    return new MyContext(optionsBuilder.Options);
  }
}
