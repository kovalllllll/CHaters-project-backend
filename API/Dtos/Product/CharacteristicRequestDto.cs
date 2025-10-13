using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class CharacteristicRequestDto
{
    [Required]
    public string Name { get; set; }
}