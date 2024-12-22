namespace API.Dtos.Product;

public class ProductCharacteristicDto
{
    public string Id { get; set; }
    public string Value { get; set; }
    public CharacteristicDto Characteristic { get; set; }
}