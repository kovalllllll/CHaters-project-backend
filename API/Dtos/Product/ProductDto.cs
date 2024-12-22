namespace API.Dtos.Product;

public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public List<ImageDto> Images { get; set; }
    public List<ProductCharacteristicDto> Characteristics { get; set; }
}