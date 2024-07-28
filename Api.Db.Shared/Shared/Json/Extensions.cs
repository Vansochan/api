using Microsoft.Extensions.DependencyInjection;

namespace Api.Shared.Shared.Json;

public static class Extensions
{
    public static IServiceCollection AddDefaultJson(this IServiceCollection services)
    {
        services.AddTransient<IDefaultJsonSerializer, DefaultJsonSerializer>();

        return services;
    }

    public static IMvcBuilder AddDefaultJsonOptions(this IMvcBuilder builder)
    {
        builder.AddNewtonsoftJson(o =>
        {
            foreach (var prop in DefaultJsonSerializerSettings.Settings.GetType().GetProperties())
            {
                o.SerializerSettings.GetType().GetProperty(prop.Name)?.SetValue(o.SerializerSettings, prop.GetValue(DefaultJsonSerializerSettings.Settings), null);
            }
        });

        return builder;
    }
}
