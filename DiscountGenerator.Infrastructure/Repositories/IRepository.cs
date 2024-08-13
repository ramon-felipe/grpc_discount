namespace GrpcDiscountGenerator.Infrastructure.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class
{
}
