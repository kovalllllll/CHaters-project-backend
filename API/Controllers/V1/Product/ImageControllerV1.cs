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
        try
        {
            _imageService.UploadImage(file, productId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllImagesByProductId([FromRoute] Guid productId)
    {
        List<Image> images;
        try
        {
            images = _imageService.GetAllImagesByProductId(productId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var response = _mapper.Map<List<ImageDto>>(images);
        return Ok(response);
    }

    [HttpGet("{imageId}")]
    public IActionResult GetImageById([FromRoute] Guid imageId)
    {
        Image image;
        try
        {
            image = _imageService.GetImageById(imageId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var response = _mapper.Map<ImageDto>(image);
        return Ok(response);
    }

    [HttpPut("{imageId}")]
    public IActionResult UpdateImage([FromRoute] Guid imageId, [FromQuery] ImageRequestDto request)
    {
        var image = _mapper.Map<Image>(request);
        image.Id = imageId;

        Image updatedImage;
        try
        {
            updatedImage = _imageService.UpdateImage(image);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var response = _mapper.Map<ImageDto>(updatedImage);
        return Ok(response);
    }

    [HttpDelete("{imageId}")]
    public IActionResult DeleteImage([FromRoute] Guid imageId)
    {
        try
        {
            _imageService.DeleteImage(imageId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }


        return NoContent();
    }
}