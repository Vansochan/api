using System.Text;
using Newtonsoft.Json;

namespace Api.Shared.Shared.Json;

public class DefaultJsonSerializer : IDefaultJsonSerializer
{
    private readonly JsonSerializerSettings _settings = DefaultJsonSerializerSettings.Settings;
    private readonly JsonSerializerSettings _snakeCaseSettings = DefaultJsonSerializerSettings.SnakeCaseSetting;
    public string Serialize(object? value)
    {
        return JsonConvert.SerializeObject(value, _settings);
    }

    public string Serialize<T>(T value)
    {
        return JsonConvert.SerializeObject(value, _settings);
    }

    public object? Deserialize(ReadOnlySpan<byte> value, Type type)
    {
        return JsonConvert.DeserializeObject(Decode(value), type, _settings);
    }

    public object? Deserialize(string value, Type type)
    {
        return JsonConvert.DeserializeObject(value, type, _settings);
    }

    public object? Deserialize(ReadOnlySpan<byte> value)
    {
        return JsonConvert.DeserializeObject(Decode(value), _settings);
    }

    public T? Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value, _settings);
    }

    public T? Deserialize<T>(Stream stream)
    {
        using var sr = new StreamReader(stream);
        using var reader = new JsonTextReader(sr);
        using var streamReader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(streamReader);

        return JsonSerializer.Create(_settings).Deserialize<T>(reader); //error
    }

    public object? Deserialize(string value)
    {
        return JsonConvert.DeserializeObject(value, _settings);
    }
    
    private static string Decode(ReadOnlySpan<byte> value)
    {
        return Encoding.UTF8.GetString(value);
    }
    public T? DeserializeResult<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value, _snakeCaseSettings);
    }

    public string SerializeResult<T>(T value)
    {
        return JsonConvert.SerializeObject(value, _snakeCaseSettings);
    }

    public TTo Map<TFrom, TTo>(TFrom from) where TFrom : class
    {
        return Deserialize<TTo>(Serialize(from))!;
    }
}

public interface IDefaultJsonSerializer
{
    string Serialize(object? value);

    string Serialize<T>(T value);

    object? Deserialize(ReadOnlySpan<byte> value, Type type);
    object? Deserialize(string value, Type type);

    object? Deserialize(ReadOnlySpan<byte> value);

    T? Deserialize<T>(Stream stream);

    T? Deserialize<T>(string value);

    object? Deserialize(string value);
    /// <summary>
    /// PascalCase to snake_case
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? DeserializeResult<T>(string value);
    /// <summary>
    /// /// PascalCase to snake_case
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string SerializeResult<T>(T value);
    /// <summary>
    /// Map object from TFrom to TTo
    /// </summary>
    /// <param name="from"></param>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <returns></returns>
    TTo Map<TFrom, TTo>(TFrom from) where TFrom : class;
}
