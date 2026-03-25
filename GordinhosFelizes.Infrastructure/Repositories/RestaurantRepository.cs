using GordinhosFelizes.Domain.Interfaces;
using GordinhosFelizes.Domain.Models;
using GordinhosFelizes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GordinhosFelizes.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly GordinhosDbContext _context;

    public RestaurantRepository(GordinhosDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await _context.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<RestaurantRanking>> GetTopRatedAsync()
    {
        return await _context.Restaurants
            .Select(r => new RestaurantRanking
            {
                RestaurantId = r.Id,
                Name = r.Name,
                AverageRating = _context.Reviews
                    .Where(x => x.RestaurantId == r.Id)
                    .Average(x => (double?)x.Rating) ?? 0,

                TotalReviews = _context.Reviews
                    .Count(x => x.RestaurantId == r.Id)
            })
            .OrderByDescending(x => x.AverageRating)
            .ThenByDescending(x => x.TotalReviews)
            .Take(10)
            .ToListAsync();
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var restaurant = await GetByIdAsync(id);
        if (restaurant != null)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}