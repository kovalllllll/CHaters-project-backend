using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
{
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<Product>();
    }

    public bool IsProductWithNameExists(string name)
    {
        return _dbSet.Any(p => p.Name == name);
    }

    public Product GetById(Guid id)
    {
        return _dbSet.Include(p => p.Images).FirstOrDefault(p => p.Id == id);
    }
    
    public IEnumerable<Product> GetAll()
    {
        return _dbSet.Include(p => p.Images).ToList();
    }
}