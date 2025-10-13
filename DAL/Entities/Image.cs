using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Image
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required] 
    public string Name { get; set; }
    [Required]
    public string ContentType { get; set; }
    [Required] 
    public string Path { get; set; }
    [Required]
    public string Bucket { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
}