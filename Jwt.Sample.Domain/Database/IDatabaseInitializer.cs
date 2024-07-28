namespace Jwt.Sample.Domain.Database;

public interface IDatabaseInitializer
{
    Task InitializeAsync();
}