namespace Application.CQRS.Authentication.Common;

public sealed record ProfileResponse(
    string Id,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string ProfilePictureUrl
);