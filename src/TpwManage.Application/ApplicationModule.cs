using Microsoft.Extensions.DependencyInjection;
using TpwManage.Application.Services.ClientService;
using TpwManage.Application.Services.ProductService;
using TpwManage.Application.Services.SellingService;

namespace TpwManage.Application;

public static class ApplicationModule
{
  public static IServiceCollection AddApplication(this IServiceCollection services) 
  {
    services.AddApplicationServices();
    return services;
  }

  private static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IClientService, ClientService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ISellingService, SellingService>();
    return services;
  }
}
