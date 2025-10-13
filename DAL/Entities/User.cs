using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [RegularExpression(@"^\+\d{12}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } 
    
    [Required]
    public DateTime UpdatedAt { get; set; } 
}