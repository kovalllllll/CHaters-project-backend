using DAL.Entities;

namespace API.Security;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    public string GenerateRefreshToken(User user);
}