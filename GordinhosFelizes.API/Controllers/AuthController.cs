using GordinhosFelizes.API.Models;
using GordinhosFelizes.Application.DTOs;
using GordinhosFelizes.Application.Services;
using GordinhosFelizes.Domain.Enums;
using GordinhosFelizes.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    private readonly ICurrentUserService _currentUser;

    public AuthController(AuthService service, ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _service.Register(dto.Name, dto.Email, dto.Password);
        return Ok(ApiResponse<string>.Ok(result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {

            var token = await _service.Login(dto.Email, dto.Password);
            var response = new { token };
            return Ok(ApiResponse<object>.Ok(response));

    }

    [HttpPut("{id}/make-admin")]
    [Authorize(Roles = nameof(Roles.admin))]
    public async Task<IActionResult> MakeAdmin(int id)
    {
        var currentUserId = _currentUser.UserId;

        await _service.MakeAdminAsync(id, currentUserId);

        return NoContent();
    }
}