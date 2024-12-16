using System.Security.Cryptography;
using DAL;
using DAL.Entities;

namespace BLL.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public void RegisterUser(string email, string password, string userName, string firstName, string lastName, string phoneNumber)
    {
        // Генерація сольового значення
        var salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        // Хешування пароля
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        var hash = pbkdf2.GetBytes(20);

        // Комбінування хешу та солі
        var hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var passwordHash = Convert.ToBase64String(hashBytes);

        // Створення користувача
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

        _context.Users.Add(user);
        _context.SaveChanges();
    }
}