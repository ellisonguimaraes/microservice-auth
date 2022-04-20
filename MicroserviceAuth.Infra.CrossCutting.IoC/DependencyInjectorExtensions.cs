using MicroserviceAuth.Domain.Application;
using MicroserviceAuth.Infra.Data.Repositories;
using MicroserviceAuth.Service.Application;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceAuth.Infra.CrossCutting.IoC;

public static class DependencyInjectorExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IRepository<Application>, ApplicationRepository>();
        services.AddTransient<IApplicationServices, ApplicationServices>();
    }
}
