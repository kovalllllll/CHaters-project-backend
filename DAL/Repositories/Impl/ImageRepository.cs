using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class ImageRepository : BaseRepository<Image, Guid>, IImageRepository
{
    private readonly DbSet<Image> _dbSet;

    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<Image>();
    }

    public bool IsImageWithNameExists(string name)
    {
        return _dbSet.Any(i => i.Name == name);
    }
}