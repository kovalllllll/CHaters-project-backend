using BLL.Exceptions;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Repositories;

namespace BLL.Services.Impl;

public class ProductCharacteristicService : IProductCharacteristicService
{
    private readonly IProductCharacteristicRepository _productCharacteristicRepository;
    private readonly IProductRepository _productRepository;
    
    public ProductCharacteristicService(IProductCharacteristicRepository productCharacteristicRepository, IProductRepository productRepository)
    {
        _productCharacteristicRepository = productCharacteristicRepository;
        _productRepository = productRepository;
    }
    
    public ProductCharacteristic CreateProductCharacteristic(ProductCharacteristic productCharacteristic , Guid productId)
    {
        var product = _productRepository.GetById(productId);
        
        productCharacteristic.ProductId = productId;
        
        var createdProductCharacteristic = _productCharacteristicRepository.Create(productCharacteristic);
        _productCharacteristicRepository.SaveChanges();

        product.ProductCharacteristics.Add(createdProductCharacteristic);
        _productRepository.Update(product);
        _productRepository.SaveChanges();
        return createdProductCharacteristic;
    }

    public List<ProductCharacteristic> GetAllProductCharacteristics(Guid productId)
    {
        var product = _productRepository.GetById(productId);
        
        var productCharacteristics = product.ProductCharacteristics.ToList();
        
        if (productCharacteristics.Count == 0)
        {
            throw new NotFoundException("No product characteristics found");
        }

        return productCharacteristics;
    }

    public ProductCharacteristic GetProductCharacteristicById(Guid id)
    {
        var productCharacteristic = _productCharacteristicRepository.GetById(id);
        
        if (productCharacteristic == null)
        {
            throw new NotFoundException($"Product characteristic with id {id} not found");
        }

        return productCharacteristic;
    }

    public ProductCharacteristic UpdateProductCharacteristic(ProductCharacteristic productCharacteristic)
    {
        var existingProductCharacteristic = _productCharacteristicRepository.GetById(productCharacteristic.Id);

        if (existingProductCharacteristic == null)
        {
            throw new NotFoundException($"Product characteristic with id {productCharacteristic.Id} not found");
        }
        
        if (!string.IsNullOrEmpty(productCharacteristic.Value))
        {
            existingProductCharacteristic.Value = productCharacteristic.Value;
        }

        if (productCharacteristic.CharacteristicId != Guid.Empty)
        {
            existingProductCharacteristic.CharacteristicId = productCharacteristic.CharacteristicId;
        }
        
        _productCharacteristicRepository.Update(existingProductCharacteristic);
        _productCharacteristicRepository.SaveChanges();

        return existingProductCharacteristic;
    }

    public void DeleteProductCharacteristic(Guid id)
    {
        _productCharacteristicRepository.Delete(id);
        
        _productCharacteristicRepository.SaveChanges();
    }
}