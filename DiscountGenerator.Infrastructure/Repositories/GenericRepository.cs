using CSharpFunctionalExtensions;
using EFCore.BulkExtensions;
using GrpcDiscountGenerator.Domain;
using Microsoft.EntityFrameworkCore;

namespace GrpcDiscountGenerator.Infrastructure.Repositories;
public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DiscountDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DiscountDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public Result Add(T entity)
    {
        _dbSet.Add(entity);
        return Result.Success();
    }

    public void Delete(int id)
    {
        var entityOrNothing = Get(id);

        if (entityOrNothing.HasNoValue)
            return;

        _dbSet.Remove(entityOrNothing.Value);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public IMaybe<T> Get(int id)
    {
        var entity = _dbSet.SingleOrDefault(_ => _.Id == id);

        return entity == null ? Maybe.None : Maybe.From(entity);
    }

    public IMaybe<T> Get(Func<T, bool> func)
    {
        var entity = _dbSet.AsNoTracking().SingleOrDefault(func);

        return entity == null ? Maybe.None : Maybe.From(entity);
    }

    public IQueryable<T> GetAll()
    {
        var entities = _dbSet.AsNoTracking();

        return entities;
    }

    public IMaybe<T> GetLast()
    {
        var entity = _dbSet.AsNoTracking().OrderBy(_ => _.Id).LastOrDefault();

        return entity == null ? Maybe.None : Maybe.From(entity);
    }

    public Result Save()
    {
        _context.SaveChanges();
        return Result.Success();
    }

    public Result Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return Result.Success();
    }

    public void BulkInsert(IEnumerable<T> entities)
    {
        _context.BulkInsert(entities);
    }
}
