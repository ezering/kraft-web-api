using Domain.Entities;

namespace Application.CQRS.Authentication.Common;

public sealed record AuthenticationResult(User User, string AccessToken);