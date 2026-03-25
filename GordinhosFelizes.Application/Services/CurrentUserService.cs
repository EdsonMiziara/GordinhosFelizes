using GordinhosFelizes.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GordinhosFelizes.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId =>
        int.Parse(_httpContextAccessor.HttpContext!
            .User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
