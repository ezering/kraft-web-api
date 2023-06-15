using Application.Auth;
using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JwtDto jwtDto) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwtDto);

    public JwtDto? Get() => _httpContextAccessor.HttpContext?.Items.TryGetValue(TokenKey, out var jwt) == true
        ? jwt as JwtDto
        : null;
}