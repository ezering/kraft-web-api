using Domain.Abstractions;
using Domain.ValueObjects.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public sealed class RoleRepository : IRoleRepository
{
    public async Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var roles = new List<Role>
        {
            Role.Admin(),
            Role.User()
        };
        return await Task.FromResult(roles);
    }

    public async Task<Role?> FindByNameAsync(Role role, CancellationToken cancellationToken)
    {
        var availableRoles = await GetAllRolesAsync(cancellationToken);
        return  availableRoles.SingleOrDefault(x => x.Value == role.Value);
    }
}