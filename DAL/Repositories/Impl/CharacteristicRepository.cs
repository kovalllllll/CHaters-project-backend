using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class CharacteristicRepository: BaseRepository<Characteristic, Guid>, ICharacteristicRepository
{
    private readonly DbSet<Characteristic> _dbSet;
    
    public CharacteristicRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<Characteristic>();
    }
    public bool IsCharacteristicWithNameExists(string name)
    {
        return _dbSet.Any(c => c.Name == name);
    }

    public IEnumerable<Characteristic> GetAll()
    {
        return _dbSet.ToList();
    }
    
    public Characteristic GetById(Guid id)
    {
        return _dbSet.FirstOrDefault(c => c.Id == id);
    }
}