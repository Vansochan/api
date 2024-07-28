using System.Data;
using Jwt.Sample.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Jwt.Sample.Infrastructure.SqlServer.Repositories;

public class DapperContext(IConfiguration configuration) : IDapperContext
{
    public IDbConnection CreateIdentityConnection() =>
        new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
}