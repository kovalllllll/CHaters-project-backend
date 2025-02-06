using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class ProductCharacteristicRequestDto
{
    [Required]
    public string Value { get; set; }
    [Required]
    public Guid CharacteristicId { get; set; }
}