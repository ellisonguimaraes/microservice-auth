using MicroserviceAuth.Domain.Mapper;

namespace MicroserviceAuth.API.Configurations;

public static class AutoMapperConfiguration
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationProfile));
    }
}
