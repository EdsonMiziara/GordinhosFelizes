using GordinhosFelizes.Domain.Models;
namespace GordinhosFelizes.Domain.Interface;

public interface IReviewRepository
{
    Task AddAsync(Review review);

    Task<List<Review>> GetByRestaurantIdAsync(int restaurantId);

    Task<double> GetAverageRatingAsync(int restaurantId);

    Task<bool> ExistsAsync(int userId, int restaurantId);

    Task<Review?> GetByIdAsync(int reviewId);
    Task UpdateAsync(Review review);
    Task DeleteAsync(int reviewId);
}
