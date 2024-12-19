using DAL.Entities;

namespace DAL.Repositories;

public interface IUserRepository
{
    User GetUserByEmail(string email);
    bool UserExists(string email);
    void AddUser(User user);
    void SaveChanges();
}