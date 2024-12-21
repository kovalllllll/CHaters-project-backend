using API.Dtos.ApiVersions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.ApiVersions;

[ApiController]
[Route("api/v1")]
public class ApiVersionControllerV1 : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ApiVersionControllerV1(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Get the version of the API
    /// </summary>
    /// <response code="200">Returns the version information</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiVersionDto), 200)]
    public IActionResult GetVersion()
    {
        var versionInfo = new ApiVersionDto
        {
            ApplicationName = _configuration["ApplicationSettings:ApplicationName"]!,
            Version = _configuration["ApplicationSettings:Version"]!,
            CurrentServerDateTime = DateTime.Now.ToString("G")
        };
        
        return Ok(versionInfo);
    }
}