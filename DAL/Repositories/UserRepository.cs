using DAL.Entities;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.SingleOrDefault(u => u.Email == email);
    }

    public bool UserExists(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}