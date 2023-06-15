using Domain.Entities;
using Domain.ValueObjects.Users;
namespace Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetUserByEmailAsync(Email email);
    Task<User?> GetUserByUsernameAsync(Username username);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<User> DeleteUserAsync(User user);
    Task<bool> EmailAlreadyExistsAsync(Email email);
    Task<bool> UsernameAlreadyExistsAsync(Username username);
}