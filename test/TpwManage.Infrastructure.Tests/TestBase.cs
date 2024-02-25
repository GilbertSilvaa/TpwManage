using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TpwManage.Infrastructure.Persistence.Context;

namespace TpwManage.Infrastructure.Tests;

public class TestBase { }

public class DbTest : IDisposable
{
  public ServiceProvider ServiceProvider { get; private set; }

  public DbTest()
  {
    string dbNameRandom = Guid.NewGuid().ToString().Replace("-", "");
    string _dbNameRandom = $"dbTpwManageTest_{dbNameRandom}";

    string conn = $"Server=localhost;Database={_dbNameRandom};Uid=root;Pwd=senha123;";
    ServiceCollection serviceCollection = new();

    serviceCollection.AddDbContext<MyContext>(o =>
      o.UseMySql(conn, ServerVersion.AutoDetect(conn)),
      ServiceLifetime.Transient);
    
    ServiceProvider = serviceCollection.BuildServiceProvider();
    using var context = ServiceProvider.GetService<MyContext>();
    context?.Database.EnsureCreated();
  }

  public void Dispose()
  {
    using var context = ServiceProvider.GetService<MyContext>();
    context?.Database.EnsureDeleted();
  }
}
