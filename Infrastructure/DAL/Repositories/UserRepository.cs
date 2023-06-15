using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = dbContext.Users;
    }
    

    public async Task<User?> GetByIdAsync(UserId id)
    {
        return await _users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(Email email)
    {
        return await _users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetUserByUsernameAsync(Username username)
    {
        return await _users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User> AddUserAsync(User user)
    {
        var entity = await _users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var entity = _users.Update(user);
        await _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<User> DeleteUserAsync(User user)
    {
        var entity = _users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> EmailAlreadyExistsAsync(Email email)
    {
        return await _users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> UsernameAlreadyExistsAsync(Username username)
    {
        return await _users.AnyAsync(x => x.Username == username);
    }
    
}