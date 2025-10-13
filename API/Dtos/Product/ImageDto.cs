using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product;

public class ImageDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public string Url { get; set; }
}