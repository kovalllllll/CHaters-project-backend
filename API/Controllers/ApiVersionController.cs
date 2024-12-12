using BLL.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/v1")]
public class ApiVersionController : ControllerBase
{
    private readonly IApiVersionService _versionService;

    public ApiVersionController(IApiVersionService versionService)
    {
        _versionService = versionService;
    }

    [HttpGet]
    public IActionResult GetVersion()
    {
        var versionInfo = _versionService.GetCurrentVersion();
        return Ok(versionInfo);
    }
}