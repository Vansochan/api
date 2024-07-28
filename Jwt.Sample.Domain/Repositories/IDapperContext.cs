using System.Data;

namespace Jwt.Sample.Domain.Repositories;

public interface IDapperContext
{
    IDbConnection CreateIdentityConnection();
}