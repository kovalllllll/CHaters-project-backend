using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class ProductRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Price { get; set; }
    public List<ImageDto> Images { get; set; }
    public List<ProductCharacteristicDto> Characteristics { get; set; }
}