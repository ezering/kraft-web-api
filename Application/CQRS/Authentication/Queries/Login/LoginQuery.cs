using Application.CQRS.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.CQRS.Authentication.Queries.Login;

public sealed record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
