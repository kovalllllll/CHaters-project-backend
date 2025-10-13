using DAL.Entities;

namespace BLL.Services;

public interface IProductService
{
    Product CreateProduct(Product product);
    List<Product> GetAllProducts();
    Product GetProductById(Guid id);
    Product UpdateProduct(Product product);
    void DeleteProduct(Guid id);
}