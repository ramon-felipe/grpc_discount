using CSharpFunctionalExtensions;

namespace GrpcDiscountGenerator.Infrastructure.Repositories;

public interface IReadRepository<out T>
{
    IMaybe<T> Get(int id);
    IMaybe<T> Get(Func<T, bool> func);
    IQueryable<T> GetAll();
}