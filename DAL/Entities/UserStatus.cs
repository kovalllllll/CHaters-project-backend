using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class UserStatus
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid(); // GUID як первинний ключ

    [Required]
    [StringLength(20)]
    public string Status { get; set; } // Status (VARCHAR(20))

    [Required]
    public Guid UserId { get; set; } // Foreign Key to User

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } // Navigation Property
}