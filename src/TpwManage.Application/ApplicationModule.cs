using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TpwManage.Application.Services.ProductService;
using TpwManage.Application.Services.SellingService;

namespace TpwManage.Application;

public static class ApplicationModule
{
  public static IServiceCollection AddApplication(this IServiceCollection services) 
  {
    services.AddApplicationServices();
    services.AddPackagesServices();
    return services;
  }

  private static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ISellingService, SellingService>();
    return services;
  }

  private static IServiceCollection AddPackagesServices(this IServiceCollection services)
  {
    services.AddMediatR(config => 
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return services;
  }
}
