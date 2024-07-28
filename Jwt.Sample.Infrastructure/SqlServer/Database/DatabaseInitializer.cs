using System.Reflection;
using DbUp;
using Jwt.Sample.Domain.Database;
using Microsoft.Extensions.Configuration;

namespace Jwt.Sample.Infrastructure.SqlServer.Database;

public class DatabaseInitializer(IConfiguration configuration) : IDatabaseInitializer
{
    public async Task InitializeAsync()
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        EnsureDatabase.For.SqlDatabase(connectionString);
        var upgrade = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        if (upgrade.IsUpgradeRequired())
        {
            upgrade.PerformUpgrade();
        }
    }
}