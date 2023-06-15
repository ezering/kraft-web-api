namespace Infrastructure.Auth;

public sealed class AuthOptions
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string SigningKey { get; set; } = default!;
    public TimeSpan? AccessTokenLifetime { get; set; }
}