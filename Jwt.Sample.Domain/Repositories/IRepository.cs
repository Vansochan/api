using System.Linq.Expressions;

namespace Jwt.Sample.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<List<T>> GetListWithPaginationAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true,
        int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}