using GordinhosFelizes.Domain.Interface;
using GordinhosFelizes.Domain.Models;
using GordinhosFelizes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GordinhosFelizes.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GordinhosDbContext _context;

    public UserRepository(GordinhosDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
