using Domain.Abstractions;
using Domain.ValueObjects.Users;
using ErrorOr;
using MediatR;

namespace Application.CQRS.Roles.Queries.GetAllRoles;

internal sealed class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQuery, ErrorOr<IEnumerable<Role>>>
{
    private readonly IRoleRepository _roleRepository;

    public GetAllRoleQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }


    public async Task<ErrorOr<IEnumerable<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleRepository.GetAllRolesAsync(cancellationToken);
    }
}