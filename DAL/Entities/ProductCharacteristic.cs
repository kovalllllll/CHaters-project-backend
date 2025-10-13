using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class ProductCharacteristic
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string Value { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    
    [Required]
    public Guid CharacteristicId { get; set; }
    
    [ForeignKey(nameof(CharacteristicId))]
    public Characteristic Characteristic { get; set; }
} 