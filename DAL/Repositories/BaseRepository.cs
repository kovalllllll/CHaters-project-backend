using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly AppDbContext _dbContext;
    
    protected BaseRepository(AppDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }
    
    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(TId id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null)
        {
            throw new NotFoundException($"{typeof(T).Name} with id {id} not found");
        }

        return entity;
    }

    public IEnumerable<T> Find(Func<T, bool> predicate, int pageNumber = 0, int pageSize = 10)
    {
        return _dbSet.Where(predicate)
            .Skip(pageSize * pageNumber)
            .Take(pageNumber)
            .ToList();
    }

    public T Create(T entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Delete(TId id)
    {
        var entity = GetById(id);
        _dbSet.Remove(entity);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}