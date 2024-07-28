using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Jwt.Sample.Swagger;

public class CorrelationIdHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Correlation-Id",
            In = ParameterLocation.Header,
            Description = "Correlation Id",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "String",
                Default = new OpenApiString(Guid.NewGuid().ToString())
            }
        });
    }
}