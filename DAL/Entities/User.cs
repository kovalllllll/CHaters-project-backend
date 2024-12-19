using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(25)]
    public string UserName { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    [MaxLength(55)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(55)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; } 
}