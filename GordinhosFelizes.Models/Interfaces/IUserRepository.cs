using GordinhosFelizes.Domain.Models;

namespace GordinhosFelizes.Domain.Interface;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task<User> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task UpdateAsync(User user);
}
