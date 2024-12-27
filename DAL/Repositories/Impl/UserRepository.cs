using DAL.Entities;
using DAL.Exceptions;
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

    public User GetByEmail(string email)
    {
        var user = _dbSet.FirstOrDefault(user => user.Email == email);

        if (user == null)
        {
            throw new EntityNotFoundException($"User with email {email} not exist");
        }

        return user;
    }
    
}