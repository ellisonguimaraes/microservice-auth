using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MicroserviceAuth.API.Extensions;

/// <summary>
/// Swagger UI Extensions
/// </summary>
public class SwaggerExtensions : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Api-Key",
            Description = "Api-Key",
            @In = ParameterLocation.Header,
            Example = OpenApiAnyFactory.CreateFromJson("\"dce6aa57-64c6-4fc0-97a2-73083373d290\""),
            Required = true,
            Schema = new OpenApiSchema { Type = "string" }
        });
    }
}
