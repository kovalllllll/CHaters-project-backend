using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Characteristic
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string Name { get; set; }
}