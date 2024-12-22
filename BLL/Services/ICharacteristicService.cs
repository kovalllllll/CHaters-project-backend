using DAL.Entities;

namespace BLL.Services;

public interface ICharacteristicService
{
    Characteristic CreateCharacteristic(Characteristic characteristic);
}