using DAL.Entities;

namespace DAL.Repositories;

public interface IProductCharacteristicRepository : IRepository<ProductCharacteristic, Guid>
{
    bool IsProductCharacteristicWithNameExists(string value);
}