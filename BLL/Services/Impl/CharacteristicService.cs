using BLL.Exceptions;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services.Impl;

public class CharacteristicService: ICharacteristicService
{
    private readonly ICharacteristicRepository _characteristicRepository;
    
    public CharacteristicService(ICharacteristicRepository characteristicRepository)
    {
        _characteristicRepository = characteristicRepository;
    }
    
    public Characteristic CreateCharacteristic(Characteristic characteristic)
    {
        if(_characteristicRepository.IsCharacteristicWithNameExists(characteristic.Name))
        {
            throw new AlreadyExistException($"Characteristic with name {characteristic.Name} already exists");
        }
        
        var createdCharacteristic = _characteristicRepository.Create(characteristic);
        
        _characteristicRepository.SaveChanges();
        
        return createdCharacteristic;
    }
}