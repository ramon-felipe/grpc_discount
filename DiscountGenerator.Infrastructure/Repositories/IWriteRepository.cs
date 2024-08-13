using CSharpFunctionalExtensions;

namespace GrpcDiscountGenerator.Infrastructure.Repositories;

public interface IWriteRepository<in T>
{
    Result Add(T entity);
    void BulkInsert(IEnumerable<T> entities);
    Result Save();
    Result Update(T entity);
    void Delete(T entity);
    void Delete(int id);
}
