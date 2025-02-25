using API.Dtos.Auth;
using API.Security;
using AutoMapper;
using BLL.Exceptions;
using BLL.Services;
using DAL.Entities;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Auth;

[ApiController]
[Route("api/v1/auth")]
public class AuthControllerV1 : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;

    public AuthControllerV1(
        IUserService userService,
        IPasswordEncoder passwordEncoder,
        IJwtTokenService jwtTokenService,
        IMapper mapper)
    {
        _userService = userService;
        _passwordEncoder = passwordEncoder;
        _jwtTokenService = jwtTokenService;
        _mapper = mapper;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request">Registration request</param>
    /// <response code="200">Returns the JWT token</response>
    /// <response code="400">User already exists</response>
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegistrationRequestDto request)
    {
        var user = _mapper.Map<DAL.Entities.User>(request);
        user.Password = _passwordEncoder.Encode(request.Password);

        DAL.Entities.User createdUser;
        try
        {
            createdUser = _userService.CreateUser(user);
        }
        catch (AlreadyExistException e)
        {
            return BadRequest(e.Message);
        }

        var accessToken = _jwtTokenService.GenerateToken(createdUser);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(createdUser);

        var response = new LoginResponseDto
        {
            Token = accessToken,
            RefreshToken = refreshToken
        };

        return Ok(response);
    }

    /// <summary>
    /// Login an existing user
    /// </summary>
    /// <param name="request">Login request</param>
    /// <response code="200">Returns the JWT token</response>
    /// <response code="401">Invalid email or password</response>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        //var user = _mapper.Map<User>(request);

        DAL.Entities.User user;
        try
        {
            user = _userService.GetUserByEmail(request.Email);
        }
        catch (NotFoundException)
        {
            return Unauthorized("Invalid email or password");
        }

        if (!_passwordEncoder.Matches(request.Password, user.Password))
        {
            return Unauthorized("Invalid email or password");
        }

        var accessToken = _jwtTokenService.GenerateToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(user);

        var response = _mapper.Map<LoginResponseDto>(user);
        response.Token = accessToken;
        response.RefreshToken = refreshToken;

        return Ok(response);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out successfully" });
    }
}