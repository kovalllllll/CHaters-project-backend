using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class ProductCharacteristicDto
{
    public string Id { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    public CharacteristicDto Characteristic { get; set; }
}