using BLL.Exceptions;
using DAL.Entities;
using DAL.Exceptions;
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
            throw new AlreadyExistException($"User with email '{user.Email}' already exists");
        }

        var createdUser = _userRepository.Create(user);

        _userRepository.SaveChanges();

        return createdUser;
    }

    public List<User> GetAllUsers()
    {
        var users = _userRepository.GetAll().ToList();
        if (users.Count == 0)
        {
            throw new NotFoundException("No users found");
        }

        return users;
    }

    public User GetUserById(Guid id)
    {
        var user = _userRepository.GetById(id);
        if (user == null)
        {
            throw new NotFoundException($"User with id {id} not found");
        }

        return user;
    }

    public User UpdateUser(User user)
    {
        var existingUser = _userRepository.GetById(user.Id);
        if (existingUser == null)
        {
            throw new NotFoundException($"User with id {user.Id} not found");
        }

        if (existingUser.Email != user.Email && _userRepository.IsUserWithEmailExists(user.Email))
        {
            throw new AlreadyExistException($"User with email '{user.Email}' already exists");
        }

        if (!string.IsNullOrEmpty(user.Email))
        {
            existingUser.Email = user.Email;
        }

        if (!string.IsNullOrEmpty(user.Password))
        {
            existingUser.Password = user.Password;
        }

        if (!string.IsNullOrEmpty(user.FirstName))
        {
            existingUser.FirstName = user.FirstName;
        }

        if (!string.IsNullOrEmpty(user.LastName))
        {
            existingUser.LastName = user.LastName;
        }

        _userRepository.Update(existingUser);
        _userRepository.SaveChanges();
        
        return existingUser;
    }

    public void DeleteUser(Guid id)
    {
        _userRepository.Delete(id);
        _userRepository.SaveChanges();
    }
}