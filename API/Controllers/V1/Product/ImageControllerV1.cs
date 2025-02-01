using AutoMapper;
using BLL.Services.Impl;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using API.Dtos.Product;
using DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace API.Controllers.V1.Product;

[ApiController]
[Route("api/v1/product/{productId}/images")]
public class ImageControllerV1 : ControllerBase
{
    private readonly ImageService _imageService;
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public ImageControllerV1(ImageService imageService, ProductService productService, IMapper mapper)
    {
        _imageService = imageService;
        _productService = productService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult UploadImage([FromForm] IFormFile file, [FromRoute] Guid productId)
    {
        // Image image;
        try
        {
            _imageService.UploadImage(file, productId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        // var response = _mapper.Map<ImageDto>(image);
        // return Ok(response);
        
        return NoContent();
    }
    
}