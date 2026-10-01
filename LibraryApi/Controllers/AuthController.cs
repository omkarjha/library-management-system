using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly MongoDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthController(MongoDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        var existing = await _context.Users.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
        if (existing is not null)
            return BadRequest(new { error = "Email already registered" });

        var allowedRoles = new[] { "Admin", "Librarian", "Member" };
        var role = allowedRoles.Contains(dto.Role) ? dto.Role : "Member";

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = role
        };

        await _context.Users.InsertOneAsync(user);

        var token = _tokenService.GenerateToken(user);
        return Ok(new AuthResponseDto(token, user.Name, user.Email, user.Role));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var user = await _context.Users.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { error = "Invalid email or password" });

        var token = _tokenService.GenerateToken(user);
        return Ok(new AuthResponseDto(token, user.Name, user.Email, user.Role));
    }
}