using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class ProductCharacteristicRepository:BaseRepository<ProductCharacteristic, Guid>, IProductCharacteristicRepository
{
    private readonly DbSet<ProductCharacteristic> _dbSet;
    
    public ProductCharacteristicRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<ProductCharacteristic>();
    }

    public bool IsProductCharacteristicWithNameExists(string value)
    {
        return _dbSet.Any(c => c.Value == value);
    }
}