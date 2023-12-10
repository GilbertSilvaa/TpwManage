using Microsoft.Extensions.DependencyInjection;
using TpwManage.Application.Services.ClientService;

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
    return services;
  }
}
