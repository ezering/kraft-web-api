using Domain.ValueObjects.Users;

namespace Application.CQRS.Authentication.Common;

public sealed record AuthenticationResponse(
    string Id,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string ProfilePictureUrl,
    string AccessToken
);