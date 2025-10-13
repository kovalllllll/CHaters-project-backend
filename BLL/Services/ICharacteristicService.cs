using DAL.Entities;

namespace BLL.Services;

public interface ICharacteristicService
{
    Characteristic CreateCharacteristic(Characteristic characteristic);
    List<Characteristic> GetAllCharacteristics();
    Characteristic GetCharacteristicById(Guid id);
    Characteristic UpdateCharacteristic(Characteristic characteristic);
    void DeleteCharacteristic(Guid id);
}