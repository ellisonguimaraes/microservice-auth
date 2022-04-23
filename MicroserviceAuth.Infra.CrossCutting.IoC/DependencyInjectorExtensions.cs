using FluentValidation;
using MicroserviceAuth.Domain;
using MicroserviceAuth.Domain.Application;
using MicroserviceAuth.Domain.DTO.Application;
using MicroserviceAuth.Domain.Validators.Application;
using MicroserviceAuth.Infra.Data.Repositories;
using MicroserviceAuth.Service.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceAuth.Infra.CrossCutting.IoC;

public static class DependencyInjectorExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // AppSettings Application Info
        var applicationInfo = configuration.GetSection("ApplicationInfo").Get<ApplicationInfo>();
        services.AddSingleton(applicationInfo);

        // Repositories
        services.AddScoped<IApplicationRepository, ApplicationRepository>();

        // Services
        services.AddScoped<IApplicationServices, ApplicationServices>();

        // Validators
        services.AddSingleton<IValidator<ApplicationRequest>, ApplicationRequestValidator>();
    }
}
