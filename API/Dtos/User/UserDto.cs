namespace API.Dtos.User;

public class UserDto
{
    public string Id { get; set; }

    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string PhoneNumber { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    
    public DateTime UpdatedAt { get; set; } 
}