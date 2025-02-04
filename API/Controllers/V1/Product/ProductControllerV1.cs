using API.Dtos.Product;
using AutoMapper;
using BLL.Exceptions;
using BLL.Services.Impl;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Product;

[ApiController]
[Route("api/v1/product")]
public class ProductControllerV1 : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public ProductControllerV1(ProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateProduct([FromQuery] ProductRequestDto request)
    {
        
        var product = _mapper.Map<DAL.Entities.Product>(request);

        DAL.Entities.Product createdProduct;
        try
        {
            createdProduct = _productService.CreateProduct(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var response = _mapper.Map<ProductDto>(createdProduct);
        return Ok(response);
    
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        List<DAL.Entities.Product> products;
        try
        {
            products = _productService.GetAllProducts();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        var response = _mapper.Map<List<ProductDto>>(products);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetProductById([FromRoute] Guid id)
    {
        DAL.Entities.Product product;
        try
        {
            product = _productService.GetProductById(id);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }

        var response = _mapper.Map<ProductDto>(product);
        return Ok(response);
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateProduct([FromRoute] Guid id, [FromQuery] ProductRequestDto request)
    {
        var product = _mapper.Map<DAL.Entities.Product>(request);
        product.Id = id;

        DAL.Entities.Product updatedProduct;
        try
        {
            updatedProduct = _productService.UpdateProduct(product);
        }
        catch (AlreadyExistException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }

        var response = _mapper.Map<ProductDto>(updatedProduct);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteProduct([FromRoute] Guid id)
    {
        try
        {
            _productService.DeleteProduct(id);
        }
        catch (NotFoundException)
        {
            //ignored
        }

        return NoContent();
    }

}