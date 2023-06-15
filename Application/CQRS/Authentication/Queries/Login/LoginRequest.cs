namespace Application.CQRS.Authentication.Queries.Login;

public sealed record LoginRequest(
    string Email,
    string Password);