using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using BLL.Services;

namespace API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(UserService userService, JwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDto request)
    {
        if (_userService.UserExists(request.Email))
        {
            return BadRequest("User already exists.");
        }

        _userService.RegisterUser(
            request.Email,
            request.Password,
            request.UserName,
            request.FirstName,
            request.LastName,
            request.PhoneNumber
        );

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        var user = _userService.AuthenticateUser(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user.Email);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Email);

        return Ok(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenType = "Bearer",
            ExpiresIn = 3600
        });
    }
}