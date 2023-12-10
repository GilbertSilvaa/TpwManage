using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Repositories;
using TpwManage.Infrastructure.Persistence;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure;

public static class InfrastructureModule
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services
      .AddRepositories()
      .AddDbConfig();
    return services;
  }

  private static IServiceCollection AddDbConfig(this IServiceCollection services)
  {
    string conn = DbConfig.ConnectionString;
    services.AddDbContext<MyContext>(options => 
      options.UseMySql(conn, ServerVersion.AutoDetect(conn)));
    return services;
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IClientRepository, ClientRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ISellingRepository, SellingRepository>();  
    return services;
  }
}
