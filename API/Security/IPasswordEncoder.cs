namespace API.Security;

public interface IPasswordEncoder
{
    string Encode(string rawPassword);
    bool Matches(string rawPassword, string encodedPassword);
}