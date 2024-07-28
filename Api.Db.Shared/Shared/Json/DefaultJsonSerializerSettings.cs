using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Shared.Shared.Json;

public static class DefaultJsonSerializerSettings
{
    public static JsonSerializerSettings Settings => new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        DateParseHandling = DateParseHandling.DateTimeOffset,
        PreserveReferencesHandling = PreserveReferencesHandling.None,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Include
    };
    public static JsonSerializerSettings SnakeCaseSetting => new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
            {
                ProcessDictionaryKeys = true
            }
        }
    };
}
