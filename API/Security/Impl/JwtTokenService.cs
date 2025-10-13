using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Security.Impl;

public class JwtTokenService : IJwtTokenService
{
    private readonly string _accessTokenSecret;
    private readonly string _refreshTokenSecret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _accessTokenExpirationTime;
    private readonly int _refreshTokenExpirationTime;

    public JwtTokenService(IConfiguration configuration)
    {
        _accessTokenSecret = configuration["Jwt:Secret"]!;
        _refreshTokenSecret = configuration["Jwt:RefreshSecret"]!;
        _issuer = configuration["Jwt:Issuer"]!;
        _audience = configuration["Jwt:Audience"]!;
        _accessTokenExpirationTime = int.Parse(configuration["Jwt:ExpirationTime"]!);
        _refreshTokenExpirationTime = int.Parse(configuration["Jwt:RefreshExpirationTime"]!);
    }

    public string GenerateToken(User user)
    {
        return GenerateJwtToken(user, _accessTokenSecret, _accessTokenExpirationTime);
    }

    public string GenerateRefreshToken(User user)
    {
        return GenerateJwtToken(user, _refreshTokenSecret, _refreshTokenExpirationTime);
    }

    private string GenerateJwtToken(User user, string secret, int expirationTime)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddMilliseconds(expirationTime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _issuer,
            Audience = _audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}