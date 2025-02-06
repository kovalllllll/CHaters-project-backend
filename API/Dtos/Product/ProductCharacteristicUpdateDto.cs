using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class ProductCharacteristicUpdateDto
{
    public string? Value { get; set; }
    public Guid CharacteristicId { get; set; }
}