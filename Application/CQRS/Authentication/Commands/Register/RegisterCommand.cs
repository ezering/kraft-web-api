using Application.CQRS.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.CQRS.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role,
    string ProfilePictureUrl) : IRequest<ErrorOr<AuthenticationResult>>;