using API.Dtos.Product;
using AutoMapper;
using BLL.Services.Impl;
using DAL.Entities;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Product;


[ApiController]
[Route("api/v1/product/{productId}/characteristics")]
public class ProductCharacteristicControllerV1 : ControllerBase
{

    private readonly ProductCharacteristicService _productCharacteristicService;
    private readonly IMapper _mapper;
    
    public ProductCharacteristicControllerV1(ProductCharacteristicService productCharacteristicService, IMapper mapper)
    {
        _productCharacteristicService = productCharacteristicService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public IActionResult CreateProductCharacteristic([FromQuery] ProductCharacteristicRequestDto request, [FromRoute] Guid productId)
    {
        
        var productCharacteristic = _mapper.Map<ProductCharacteristic>(request);
        
        ProductCharacteristic createdProductCharacteristic;
        try
        {
            createdProductCharacteristic = _productCharacteristicService.CreateProductCharacteristic(productCharacteristic, productId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        var response = _mapper.Map<ProductCharacteristicDto>(createdProductCharacteristic);
        
        return Ok(response);
    }
    
    [HttpGet]
    public IActionResult GetAllProductCharacteristics([FromRoute] Guid productId)
    {
        List<ProductCharacteristic> productCharacteristics;
        try
        {
            productCharacteristics = _productCharacteristicService.GetAllProductCharacteristics(productId);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        var response = _mapper.Map<List<ProductCharacteristicDto>>(productCharacteristics);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{productCharacteristicId}")]
    public IActionResult GetProductCharacteristicById([FromRoute] Guid productCharacteristicId)
    {
        ProductCharacteristic productCharacteristic;
        try
        {
            productCharacteristic = _productCharacteristicService.GetProductCharacteristicById(productCharacteristicId);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        var response = _mapper.Map<ProductCharacteristicDto>(productCharacteristic);
        return Ok(response);
    }
    
    [HttpPut]
    [Route("{productCharacteristicId}")]
    public IActionResult UpdateProductCharacteristic([FromRoute] Guid productCharacteristicId, [FromQuery] ProductCharacteristicUpdateDto request)
    {
        var productCharacteristic = _mapper.Map<ProductCharacteristic>(request);
        productCharacteristic.Id = productCharacteristicId;

        ProductCharacteristic updatedProductCharacteristic;
        try
        {
            updatedProductCharacteristic = _productCharacteristicService.UpdateProductCharacteristic(productCharacteristic);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        var response = _mapper.Map<ProductCharacteristicDto>(updatedProductCharacteristic);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{productCharacteristicId}")]
    
    public IActionResult DeleteProductCharacteristic([FromRoute] Guid productCharacteristicId)
    {
        try
        {
            _productCharacteristicService.DeleteProductCharacteristic(productCharacteristicId);
        }
        catch (NotFoundException e)
        {
            //ignored
        }
        
        return NoContent();
    }
}