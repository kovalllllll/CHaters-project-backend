using DAL.Entities;

namespace BLL.Services;

public interface IUserService
{
    User CreateUser(User user);
    public List<User> GetAllUsers();
    public User GetUserByEmail(string email);
    public User GetUserById(Guid id);
    public User UpdateUser(User user);
    public void DeleteUser(Guid id);
}