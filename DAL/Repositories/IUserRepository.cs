using DAL.Entities;

namespace DAL.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    bool IsUserWithEmailExists(string email);
}