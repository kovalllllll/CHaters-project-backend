using DAL.Entities;
using Microsoft.AspNetCore.Http;
namespace BLL.Services;

public interface IImageService
{
    void UploadImage(IFormFile file,Guid productId);
    List<Image> GetAllImagesByProductId(Guid productId);
    Image GetImageById(Guid imageId);
    Image UpdateImage(Image image);
    void DeleteImage(Guid imageId);
}