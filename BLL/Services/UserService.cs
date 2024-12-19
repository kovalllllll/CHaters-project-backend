using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BLL.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterUser(string email, string password, string userName, string firstName, string lastName, string phoneNumber)
    {
        var passwordHash = HashPassword(password);

        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _userRepository.AddUser(user);
        _userRepository.SaveChanges();
    }

    public User AuthenticateUser(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
        {
            return null;
        }
        return user;
    }

    public bool UserExists(string email)
    {
        return _userRepository.UserExists(email);
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        var hashBytes = Convert.FromBase64String(storedHash);
        var salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var hashToCompare = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 20
        );

        return hashBytes.Skip(16).SequenceEqual(hashToCompare);
    }

    private string HashPassword(string password)
    {
        var salt = new byte[16];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        var hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 20
        );

        var hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }
}