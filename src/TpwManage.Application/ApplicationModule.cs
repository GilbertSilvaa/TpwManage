using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TpwManage.Application;

public static class ApplicationModule
{
  public static IServiceCollection AddApplication(this IServiceCollection services) 
  {
    services.AddPackagesServices();
    return services;
  }

  private static IServiceCollection AddPackagesServices(this IServiceCollection services)
  {
    services.AddMediatR(config => 
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return services;
  }
}
