using API.Dtos.Auth;
using API.Security;
using AutoMapper;
using BLL.Exceptions;
using BLL.Services;
using DAL.Entities;
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
        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncoder.Encode(request.Password);
        
        User createdUser;
        try
        {
            createdUser = _userService.CreateUser(user);
        }
        catch (AlreadyExistException e)
        {
            return BadRequest(e.Message);
        }
        
        var token = _jwtTokenService.GenerateToken(createdUser);
        var response = new LoginResponseDto
        {
            UserId = createdUser.Id.ToString(),
            Email = createdUser.Email,
            Token = token
        };
        
        return Ok(response);
    }
}