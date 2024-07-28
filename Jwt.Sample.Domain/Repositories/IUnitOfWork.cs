namespace Jwt.Sample.Domain.Repositories;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
}