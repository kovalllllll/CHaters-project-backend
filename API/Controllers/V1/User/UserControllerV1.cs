
using API.Dtos.User;
using API.Mappers.Users;
using AutoMapper;
using BLL.Services;
using DAL.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.User;
[ApiController]
[Route("api/v1/users")]
public class UserControllerV1 : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UserControllerV1(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        List<DAL.Entities.User> users;
        try
        {
            users = _userService.GetAllUsers();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        var response = _mapper.Map<List<UserDto>>(users);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        DAL.Entities.User user;
        try
        {
            user = _userService.GetUserById(id);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        var response = _mapper.Map<UserDto>(user);
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] UserUpdateDto updateUserDto)
    {
        var user = _mapper.Map<DAL.Entities.User>(updateUserDto);
        user.Id = id;
        
        DAL.Entities.User updatedUser;
        try
        {
            updatedUser = _userService.UpdateUser(user);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        var response = _mapper.Map<UserDto>(updatedUser);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        try
        {
            _userService.DeleteUser(id);
        }
        catch (NotFoundException)
        {
            //ignored
        }
        return NoContent();
    }
}