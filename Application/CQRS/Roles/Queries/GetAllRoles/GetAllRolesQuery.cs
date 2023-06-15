using Domain.ValueObjects.Users;
using ErrorOr;
using MediatR;
namespace Application.CQRS.Roles.Queries.GetAllRoles;

public record GetAllRolesQuery() : IRequest<ErrorOr<IEnumerable<Role>>>;