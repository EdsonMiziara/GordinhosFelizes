using GordinhosFelizes.Domain.Interface;
using GordinhosFelizes.Domain.Models;
using GordinhosFelizes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly GordinhosDbContext _context;

    public ReviewRepository(GordinhosDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Review>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Reviews
            .Where(r => r.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingAsync(int restaurantId)
    {
        return await _context.Reviews
            .Where(r => r.RestaurantId == restaurantId)
            .AverageAsync(r => (double?)r.Rating) ?? 0;
    }

    public async Task<bool> ExistsAsync(int userId, int restaurantId)
    {
        return await _context.Reviews
            .AnyAsync(r => r.UserId == userId && r.RestaurantId == restaurantId);
    }

    public async Task<Review?> GetByIdAsync(int reviewId)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }

    public async Task UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int reviewId)
    {
        var review = await GetByIdAsync(reviewId);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}