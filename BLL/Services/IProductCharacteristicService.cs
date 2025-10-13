using DAL.Entities;

namespace BLL.Services;

public interface IProductCharacteristicService
{
    ProductCharacteristic CreateProductCharacteristic(ProductCharacteristic productCharacteristic , Guid productId);
    List<ProductCharacteristic> GetAllProductCharacteristics(Guid productId);
    ProductCharacteristic GetProductCharacteristicById(Guid id);
    ProductCharacteristic UpdateProductCharacteristic(ProductCharacteristic productCharacteristic);
    void DeleteProductCharacteristic(Guid id);
}