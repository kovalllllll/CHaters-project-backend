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
    }
}