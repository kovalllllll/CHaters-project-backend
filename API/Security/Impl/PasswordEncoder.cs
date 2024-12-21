using System.Security.Cryptography;
using System.Text;

namespace API.Security.Impl;

public class PasswordEncoder : IPasswordEncoder
{
    private readonly string _secret;
    private readonly int _iterations;
    private readonly int _keyLength;
    
    public PasswordEncoder(IConfiguration configuration)
    {
        _secret = configuration["PasswordEncoder:Secret"]!;
        _iterations = int.Parse(configuration["PasswordEncoder:Iterations"]!);
        _keyLength = int.Parse(configuration["PasswordEncoder:KeyLength"]!);
    }
    
    public string Encode(string rawPassword)
    {
        using var rfc2898 = new Rfc2898DeriveBytes(
            rawPassword,
            Encoding.UTF8.GetBytes(_secret),
            _iterations,
            HashAlgorithmName.SHA512);
        return Convert.ToBase64String(rfc2898.GetBytes(_keyLength));
    }
    
    public bool Matches(string rawPassword, string encodedPassword)
    {
        return Encode(rawPassword) == encodedPassword;
    }
}