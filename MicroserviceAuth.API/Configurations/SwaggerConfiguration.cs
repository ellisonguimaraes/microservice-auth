using MicroserviceAuth.API.Extensions;
using MicroserviceAuth.Infra.CrossCutting.IoC;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MicroserviceAuth.API.Configurations;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Auth API",
                Version = "v1",
                Description = "Auth API from Universidade Estadual de Santa Cruz (UESC)",
                Contact = new OpenApiContact
                {
                    Name = "Ellison W. M. Guimaraes",
                    Email = "ellison.guimaraes@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/ellisonguimaraes/")
                }
            });

            options.OperationFilter<SwaggerExtensions>();

            // Configure Authentication Support in Swagger Page
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                                        Enter 'Bearer' [space] and then your token in the text input below.
                                        Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            // Configure XML Comments to Swagger
            var xmlFileAPI = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFileDomain = "MicroserviceAuth.Domain.xml";
            var xmlPathAPI = Path.Combine(AppContext.BaseDirectory, xmlFileAPI);
            var xmlPathDomain = Path.Combine(AppContext.BaseDirectory, xmlFileDomain);
            options.IncludeXmlComments(xmlPathAPI);
            options.IncludeXmlComments(xmlPathDomain);
        });
    }
}
