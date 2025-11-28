using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wave.API.Application.DTOs;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Enums;
using Wave.API.Domain.Interfaces;

namespace Wave.API.WebApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UserController(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository,
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {

        var newUser = await _userRepository.GetByEmail(request.Email);

        if(newUser != null)
            return Conflict("A user with this email already exists.");

        newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            Type = Enum.IsDefined(typeof(UserType), request.type) ? (UserType)request.type : UserType.Regular,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.Add(newUser);
        
        await _userRepository.SaveChanges();
        
        return CreatedAtAction(nameof(CreateUser), new { id = newUser.Id }, newUser);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
            return NotFound();
        
        return Ok(user);
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAll();

        return Ok(users);
    }

    [HttpPost("login")]
    public async Task<AuthResponse> LoginUser(LoginRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user == null)
          return new AuthResponse(null, "Usuário ou senha inválidos.");

        if (!_userService.VerifyPassword(request.Password, user.PasswordHash))
          return new AuthResponse(null, "Usuário ou senha inválidos.");

        var token = _userService.GenerateJwtToken(user);

        return new AuthResponse(token, null);       
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user == null)
            return NotFound();
        
        if (!string.IsNullOrEmpty(request.UserName))
            user.Name = request.UserName;
        
        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;

        if (!string.IsNullOrEmpty(request.PasswordHash))
            user.PasswordHash = request.PasswordHash;

        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();
        
        return Ok("Informações alteradas com sucesso");
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
            return NotFound();

        await _userRepository.Delete(user);

        await _userRepository.SaveChanges();
        
        return NoContent();
    }
}
