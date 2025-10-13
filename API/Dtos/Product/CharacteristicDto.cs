using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class CharacteristicDto
{
    public string Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}