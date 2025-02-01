using DAL.Entities;

namespace DAL.Repositories;

public interface ICharacteristicRepository : IRepository<Characteristic, Guid>
{
    bool IsCharacteristicWithNameExists(string name);
}