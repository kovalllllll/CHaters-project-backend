using API.Dtos.Product;
using AutoMapper;
using BLL.Services.Impl;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Product;

[ApiController]
[Route("api/v1/product")]
public class CharacteristicControllerV1 : ControllerBase
{
    private readonly CharacteristicService _characteristicService;
    private readonly IMapper _mapper;

    public CharacteristicControllerV1(CharacteristicService characteristicService, IMapper mapper)
    {
        _characteristicService = characteristicService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("characteristic")]
    public IActionResult CreateCharacteristic([FromQuery] string name)
    {
        var characteristic = _mapper.Map<Characteristic>(name);
    
        Characteristic createdCharacteristic;
        try
        {
            createdCharacteristic = _characteristicService.CreateCharacteristic(characteristic);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    
        var response = _mapper.Map<CharacteristicDto>(createdCharacteristic);

        return Ok(response);
    }
}