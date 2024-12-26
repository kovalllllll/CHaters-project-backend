using DAL.Entities;

namespace DAL.Repositories;

public interface IProductRepository:IRepository<Product, Guid> 
{
    bool IsProductWithNameExists(string name);
}