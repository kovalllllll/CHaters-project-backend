using BLL.Exceptions;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Repositories;

namespace BLL.Services.Impl;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Product CreateProduct(Product product)
    {
        if (_productRepository.IsProductWithNameExists(product.Name))
        {
            throw new AlreadyExistException($"Product with name {product.Name} already exists");
        }
        
        var createdProduct = _productRepository.Create(product);
        
        _productRepository.SaveChanges();
        
        return createdProduct;
    }

    public List<Product> GetAllProducts()
    {
        var products = _productRepository.GetAll().ToList();
        
        if (products.Count == 0)
        {
            throw new Exception("No products found");
        }
        
        return products;
    }

    public Product GetProductById(Guid id)
    {
        var product = _productRepository.GetById(id);
        
        if (product == null)
        {
            throw new Exception($"Product with id {id} not found");
        }
        
        return product;
    }

    public Product UpdateProduct(Product product)
    {
        var existingProduct = _productRepository.GetById(product.Id);
        
        if (existingProduct == null)
        {
            throw new NotFoundException($"Product with id {product.Id} not found");
        }
        if (_productRepository.IsProductWithNameExists(product.Name))
        {
            throw new AlreadyExistException($"Product with name {product.Name} already exists");
        }
        
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        
        _productRepository.Update(existingProduct);
        _productRepository.SaveChanges();
        
        return existingProduct;
    }

    public void DeleteProduct(Guid id)
    {
        _productRepository.Delete(id);
        
        _productRepository.SaveChanges();
    }
}