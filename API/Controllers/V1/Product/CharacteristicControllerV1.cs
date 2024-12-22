using API.Dtos.Product;
using AutoMapper;
using BLL.Services.Impl;
using DAL.Entities;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Product;

[ApiController]
[Route("api/v1/product/characteristics")]
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

    [HttpGet]
    public IActionResult GetAllCharacteristics()
    {
        try
        {
            var characteristics = _characteristicService.GetAllCharacteristics();
            var response = _mapper.Map<List<CharacteristicDto>>(characteristics);

            return Ok(response);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCharacteristicById([FromRoute] Guid id)
    {
        Characteristic characteristic;
        try
        {
            characteristic = _characteristicService.GetCharacteristicById(id);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }

        var response = _mapper.Map<CharacteristicDto>(characteristic);
        return Ok(response);
    }
}