using DAL.Entities;
using Microsoft.AspNetCore.Http;
namespace BLL.Services;

public interface IImageService
{
    void UploadImage(IFormFile file,Guid productId);
}