namespace GordinhosFelizes.Application.Services;

using GordinhosFelizes.Domain.Enums;
using GordinhosFelizes.Domain.Exceptions;
using GordinhosFelizes.Domain.Interface;
using GordinhosFelizes.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<string> Register(string name, string email, string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User(name, email, hash, Roles.user);

        await _userRepository.AddAsync(user);

        return "Usuário criado";
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new UnauthorizedException("Credenciais inválidas");

        return GenerateToken(user);
    }
    public async Task MakeAdminAsync(int userId, int currentUserId)
    {
        if (userId == currentUserId)
            throw new Exception("Você não pode promover a si mesmo");

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        user.Role = Roles.user;

        await _userRepository.UpdateAsync(user);
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}