using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions.Time;
using Application.Auth;
using Application.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{

    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan _accessTokenLifetime;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();


    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _accessTokenLifetime = options.Value.AccessTokenLifetime ?? TimeSpan.FromHours(1);
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
            SecurityAlgorithms.HmacSha256Signature);
    }

    public JwtDto CreateToken(Guid userId, List<string> role)
    {
        var now = _clock.Current();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(ClaimTypes.Role, string.Join(',', role)),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };
        var expires = now.Add(_accessTokenLifetime);
        var jwt = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            now,
            expires,
            _signingCredentials);
        var token = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = token,
        };
    }
}