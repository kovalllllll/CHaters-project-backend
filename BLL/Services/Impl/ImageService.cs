using DAL.Entities;
using DAL.Exceptions;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Impl;

public class ImageService : IImageService
{
    private readonly string _bucket;
    private readonly IImageRepository _imageRepository;
    private readonly IProductRepository _productRepository;

    public ImageService(string bucket, IImageRepository imageRepository, IProductRepository productRepository)
    {
        _bucket = bucket;
        _imageRepository = imageRepository;
        _productRepository = productRepository;

        if (!Directory.Exists(_bucket))
        {
            Directory.CreateDirectory(_bucket);
        }
    }

    public void UploadImage(IFormFile file, Guid productId)
    {
        var product = _productRepository.GetById(productId);
        var imageEntity = new Image
        {
            Name = file.FileName,
            ContentType = file.ContentType,
            Bucket = _bucket,
            Path = $"{product.Id}/{Guid.NewGuid()}-{file.FileName}",
            ProductId = product.Id
        };

        if (_imageRepository.IsImageWithNameExists(imageEntity.Name))
        {
            throw new AlreadyExistsException($"Image with name {imageEntity.Name} already exists");
        }

        var filePath = Path.Combine(_bucket, imageEntity.Path);

        var directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        imageEntity = _imageRepository.Create(imageEntity);
        _imageRepository.SaveChanges();
        
        product.Images.Add(imageEntity);
        _productRepository.Update(product);
        _productRepository.SaveChanges();
    }

    public List<Image> GetAllImagesByProductId(Guid productId)
    {
        var product = _productRepository.GetById(productId);
        
        if (product == null)
        {
            throw new NotFoundException($"Product with id {productId} not found");
        }
        
        return product.Images.ToList();
    }

    public Image GetImageById(Guid imageId)
    {
        var image = _imageRepository.GetById(imageId);

        if (image == null)
        {
            throw new NotFoundException($"Image with id {imageId} not found");
        }

        return image;
    }

    public Image UpdateImage(Image image)
    {
        var imageEntity = _imageRepository.GetById(image.Id);
        
        if(imageEntity == null)
        {
            throw new NotFoundException($"Image with id {image.Id} not found");
        }
        if(_imageRepository.IsImageWithNameExists(image.Name))
        {
            throw new AlreadyExistsException($"Image with name {image.Name} already exists");
        }
        
        imageEntity.Name = image.Name;

        imageEntity = _imageRepository.Update(imageEntity);
        _imageRepository.SaveChanges();

        return imageEntity;
    }

    public void DeleteImage(Guid imageId)
    {
        _imageRepository.Delete(imageId);
        _imageRepository.SaveChanges();
    }
}