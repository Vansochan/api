using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jwt.Sample.Infrastructure.SqlServer.Converters;

public class EnumListToAArrayConverter
{
    public static ValueConverter<List<TEnum>, string[]> Create<TEnum>()
        where TEnum : Enum
    {
        return new ValueConverter<List<TEnum>, string[]>(
            v => v.Select(e => e.ToString()).ToArray(),
            v => v.Select(e => (TEnum)Enum.Parse(typeof(TEnum), e)).ToList());
    }
}