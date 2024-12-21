using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<User>();
    }

    public bool IsUserWithEmailExists(string email)
    {
        return _dbSet.Any(user => user.Email == email);
    }
}