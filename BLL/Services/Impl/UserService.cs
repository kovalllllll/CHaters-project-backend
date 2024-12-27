using BLL.Exceptions;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User GetUserByEmail(string email)
    {
        return _userRepository.GetByEmail(email);
    }
    
    public User CreateUser(User user)
    {
        if (_userRepository.IsUserWithEmailExists(user.Email))
        {
            throw new UserAlreadyExistException($"User with email '{user.Email}' already exists");
        }
        
        var createdUser = _userRepository.Create(user);
        
        _userRepository.SaveChanges();
        
        return createdUser;
    }


}