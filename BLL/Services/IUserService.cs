using DAL.Entities;

namespace BLL.Services;

public interface IUserService
{
    User CreateUser(User user);
    public User GetUserByEmail(string email);
}