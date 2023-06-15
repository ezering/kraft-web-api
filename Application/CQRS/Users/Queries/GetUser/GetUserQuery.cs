using Application.CQRS.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.CQRS.Users.Queries.GetUser;

public sealed record GetUserQuery(
    Guid Id
    ) : IRequest<ErrorOr<GetUserResponse>>;