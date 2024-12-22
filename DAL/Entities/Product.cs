using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Product
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] 
    public string Name { get; set; }

    [Required] 
    public int Price { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<ProductCharacteristic> ProductCharacteristics { get; set; } = new List<ProductCharacteristic>();
}