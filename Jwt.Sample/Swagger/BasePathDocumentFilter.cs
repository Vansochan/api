using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Jwt.Sample.Swagger;

public class BasePathDocumentFilter(IConfiguration configuration) : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var urls = configuration.GetSection("Swagger:Urls").Get<string[]>() ?? [];
        swaggerDoc.Servers = urls
            .Select(x => new OpenApiServer { Url = x })
            .ToList();
    }
}