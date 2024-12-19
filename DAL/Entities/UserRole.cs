using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class UserRole // UserRole
{
    [Key]
    public Guid Id { get; set; } // Primary Key // Id

    [Required]
    public Guid UserId { get; set; } // Foreign Key to User

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } // Navigation Property

    [Required]
    public Guid RoleId { get; set; } // Foreign Key to Role

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } // Navigation Property
}