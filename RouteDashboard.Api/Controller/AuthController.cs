
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RouteDashboard.Api.Model;
using RouteDashboard.Api.Services;


namespace RouteDashboard.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly MongoDbService _mongoDb;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthController(MongoDbService mongoDb)
    {
        _mongoDb = mongoDb;
        _passwordHasher = new PasswordHasher<User>();
    }

    // Cadastro
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var users = _mongoDb.GetCollection<User>("Users");

        var existingUser = await users.Find(
            user => user.Email == email
        ).FirstOrDefaultAsync();

        if (existingUser != null)
        {
            return Conflict(new
            {
                message = "Este e-mail já está cadastrado."
            });
        }

        var user = new User
        {
            Name = request.Name.Trim(),
            Email = email
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.Password
        );

        await users.InsertOneAsync(user);

        return Ok(new
        {
            message = "Usuário cadastrado com sucesso!",
            user = new
            {
                user.Id,
                user.Name,
                user.Email
            }
        });
    }

    // Login 
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var users = _mongoDb.GetCollection<User>("Users");

        var user = await users.Find(
            item => item.Email == email
        ).FirstOrDefaultAsync();

        if (user == null)
        {
            return Unauthorized(new
            {
                message = "E-mail ou senha incorretos."
            });
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new
            {
                message = "E-mail ou senha incorretos."
            });
        }

        return Ok(new
        {
            message = "Login realizado com sucesso!",
            user = new
            {
                user.Id,
                user.Name,
                user.Email
            }
        });
    }
}

// recebidos do cadastro
public class RegisterRequest
{
    [System.ComponentModel.DataAnnotations.Required]
    public string Name { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.EmailAddress]
    public string Email { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.MinLength(8)]
    public string Password { get; set; } = string.Empty;
}

// recebidos do login
public class LoginRequest
{
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.EmailAddress]
    public string Email { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required]
    public string Password { get; set; } = string.Empty;
}
