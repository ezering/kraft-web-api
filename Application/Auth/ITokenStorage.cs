using Application.DTO;

namespace Application.Auth;

public interface ITokenStorage
{
    void Set(JwtDto jwtDto);
    JwtDto? Get();
}