namespace Application.CQRS.Authentication.Commands.Register;

public sealed record RegisterRequest(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role,
    string ProfilePictureUrl
    );