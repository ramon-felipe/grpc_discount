using CSharpFunctionalExtensions;

namespace GrpcDiscountGenerator.Infrastructure.Repositories;

public interface IWriteRepository<in T>
{
    Result Add(T entity);
    Result Save();
    Result Update(T entity);
    void Delete(int id);
}
