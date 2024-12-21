namespace DAL.Repositories;

public interface IRepository<T, TId> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(TId id);
    IEnumerable<T> Find(Func<T, bool> predicate, int pageNumber = 0, int pageSize = 10);
    T Create(T entity);
    T Update(T entity);
    void Delete(T entity);
    void Delete(TId id);
    void SaveChanges();
}