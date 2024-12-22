using BLL.Exceptions;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Repositories;

namespace BLL.Services.Impl;

public class CharacteristicService : ICharacteristicService
{
    private readonly ICharacteristicRepository _characteristicRepository;

    public CharacteristicService(ICharacteristicRepository characteristicRepository)
    {
        _characteristicRepository = characteristicRepository;
    }

    public Characteristic CreateCharacteristic(Characteristic characteristic)
    {
        if (_characteristicRepository.IsCharacteristicWithNameExists(characteristic.Name))
        {
            throw new AlreadyExistException($"Characteristic with name {characteristic.Name} already exists");
        }

        var createdCharacteristic = _characteristicRepository.Create(characteristic);

        _characteristicRepository.SaveChanges();

        return createdCharacteristic;
    }

    public List<Characteristic> GetAllCharacteristics()
    {
        var characteristics = _characteristicRepository.GetAll().ToList();
        
        if (characteristics.Count == 0)
        {
            throw new NotFoundException("No characteristics found");
        }

        return characteristics;
    }

    public Characteristic GetCharacteristicById(Guid id)
    {
        var characteristic = _characteristicRepository.GetById(id);
        
        if (characteristic == null)
        {
            throw new NotFoundException($"Characteristic with id {id} not found");
        }

        return characteristic;
    }
}