using MicroserviceAuth.Domain;
using MicroserviceAuth.Infra.Data.Repositories;
using MicroserviceAuth.Service.Application;

namespace MicroserviceAuth.API.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IApplicationRepository _applicationRepository;

    private readonly ApplicationInfo _applicationInfo;

    public ApiKeyMiddleware(RequestDelegate next, ApplicationInfo applicationInfo)
    {
        _next = next;
        _applicationInfo = applicationInfo;
    }

    public async Task Invoke(HttpContext context, IApplicationServices applicationServices)
    {
        var apiKey = context.Request.Headers["api-key"].ToString();

        if (string.IsNullOrWhiteSpace(apiKey) || !await applicationServices.ValidateApiKeyByAppNameAsync(_applicationInfo.Name, apiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}
