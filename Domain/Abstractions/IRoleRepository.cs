using Domain.ValueObjects.Users;

namespace Domain.Abstractions;

public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
    Task<Role?> FindByNameAsync(Role role, CancellationToken cancellationToken);
}