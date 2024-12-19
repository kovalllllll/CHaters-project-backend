using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Role
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid(); // GUID як первинний ключ

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } // Name (VARCHAR(30))
}