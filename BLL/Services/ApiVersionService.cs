using BLL.Service.Base;
using DAL.Entity;
using Microsoft.Extensions.Configuration;

namespace BLL.Service;

public class ApiVersionService : IApiVersionService
{
    private readonly IConfiguration _configuration;

    public ApiVersionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApiVersionInfo GetCurrentVersion()
    {
        return new ApiVersionInfo
        {
            ApplicationName = _configuration["ApplicationSettings:ApplicationName"],
            Version = _configuration["ApplicationSettings:Version"],
            BuildDate = File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("G")
        };
    }
}