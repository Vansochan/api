using Jwt.Sample.Domain.Repositories;

namespace Jwt.Sample.Infrastructure.SqlServer.Repositories;

public sealed class UnitOfWork(DefaultDbContext context) : IUnitOfWork, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        return new Repository<T>(context);
    }
}