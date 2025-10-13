using DAL.Entities;

namespace DAL.Repositories;

public interface IImageRepository : IRepository<Image, Guid>
{
    bool IsImageWithNameExists(string name);
}