namespace Application.CQRS.Users.Common;

public sealed record GetUserResponse(
    Guid Id,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string ProfilePictureUrl
);