using DAL.Entity;

namespace BLL.Service.Base;

public interface IApiVersionService
{
    ApiVersionInfo GetCurrentVersion();
}