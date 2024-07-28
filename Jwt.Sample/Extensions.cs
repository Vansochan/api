using Api.Shared.Constants;
using Api.Shared.Shared.Json;
using Correlate;
using FluentValidation;
using Jwt.Sample.Application.Common;
using Jwt.Sample.Application.DTO;
using Jwt.Sample.Infrastructure;
using Jwt.Sample.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Jwt.Sample;

public static class Extensions
{
    private static IServiceCollection AddDefaultSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity api document", Version = "v1" });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = "oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            var xmlFiles = new List<string>();
            configuration.GetSection("Swagger:XmlFiles").Bind(xmlFiles);
            foreach (var xml in xmlFiles)
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));
            }

            options.DocumentFilter<BasePathDocumentFilter>();
            options.OperationFilter<CorrelationIdHeaderFilter>();
        });

        return services;
    }
    
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddDefaultSwagger(configuration);

        services.AddDefaultJson();

        // add controller and json
        services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    // Access the HttpContext
                    var httpContext = context.HttpContext;

                    // Retrieve the ICorrelationContextAccessor from the HttpContext
                    var correlationAccessor = httpContext.RequestServices.GetService<ICorrelationContextAccessor>();

                    // Now you can use the correlationAccessor to get the Correlation ID
                    var correlationId = correlationAccessor?.CorrelationContext.CorrelationId;

                    // Use the correlationId as needed, for example, include it in the response
                    var error = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .Select(e => new FailedResultDto()
                        {
                            Name = e.Key,
                            Message = e.Value?.Errors.FirstOrDefault()?.ErrorMessage
                        });

                    var errorResult = ResultResponse.FailedResult<List<FailedResultDto>>(
                        result: error.ToList(),
                        code: DefaultResponseCode.WrongParam,
                        message: "Validation failed",
                        correlationId: correlationId);

                    return new UnprocessableEntityObjectResult(errorResult.Response);
                };
            })
            .AddDefaultJsonOptions();

        services.AddInfrastructure(configuration);

        // register fluent validation
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        
        // add mediator
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            /*cfg.AddOpenBehavior(typeof(MobileValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));*/
        });

        return services;
    }
}