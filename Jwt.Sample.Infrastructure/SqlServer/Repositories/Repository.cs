using System.Linq.Expressions;
using Jwt.Sample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Thinktecture;
using Thinktecture.EntityFrameworkCore;

namespace Jwt.Sample.Infrastructure.SqlServer.Repositories;

public class Repository<T>(DefaultDbContext context) : IRepository<T> where T : class
{
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();
        if (asNoTracking)
        {
            query = query
                .AsNoTracking()
                .WithTableHints(SqlServerTableHint.NoLock);
        }

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();
        if (asNoTracking)
        {
            query = query
                .AsNoTracking()
                .WithTableHints(SqlServerTableHint.NoLock);
        }

        return await query.Where(predicate: predicate).ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetListWithPaginationAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true,
        int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();
        if (asNoTracking)
        {
            query = query
                .AsNoTracking()
                .WithTableHints(SqlServerTableHint.NoLock);
        }

        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return await query.Where(predicate: predicate).ToListAsync(cancellationToken);
    }
}