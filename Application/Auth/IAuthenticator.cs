using Application.DTO;

namespace Application.Auth;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, List<string> role);
}