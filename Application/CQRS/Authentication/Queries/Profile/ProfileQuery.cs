using Application.CQRS.Authentication.Common;
using MediatR;
using ErrorOr;

namespace Application.CQRS.Authentication.Queries.Profile;

public record ProfileQuery(
    Guid UserId 
    ) : IRequest<ErrorOr<ProfileResponse>>;