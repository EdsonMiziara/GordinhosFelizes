using GordinhosFelizes.Domain.Models;

namespace GordinhosFelizes.Domain.Interfaces;

public interface IRestaurantRepository
{
    Task AddAsync(Restaurant restaurant);

    Task<List<Restaurant>> GetAllAsync();

    Task<Restaurant?> GetByIdAsync(int id);

    Task<List<RestaurantRanking>> GetTopRatedAsync();

    Task UpdateAsync(Restaurant restaurant);

    Task DeleteAsync(int id);
}