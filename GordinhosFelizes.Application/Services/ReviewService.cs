using GordinhosFelizes.Domain.Exceptions;
using GordinhosFelizes.Domain.Interface;
using GordinhosFelizes.Domain.Models;

public class ReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository repository)
    {
        _repository = repository;
    }
    public async Task CreateAsync(Review review)
    {
        var exists = await _repository.ExistsAsync(review.UserId, review.RestaurantId);

        if (exists)
            throw new BusinessException("Você já avaliou este restaurante");

        await _repository.AddAsync(review);
    }

    public async Task<List<Review>> GetByRestaurantAsync(int restaurantId)
    {
        return await _repository.GetByRestaurantIdAsync(restaurantId);
    }

    public async Task<double> GetAverageAsync(int restaurantId)
    {
        return await _repository.GetAverageRatingAsync(restaurantId);
    }
    public async Task UpdateAsync(int reviewId, int userId, int rating, string comment)
    {
        var review = await _repository.GetByIdAsync(reviewId);

        if (review == null)
            throw new NotFoundException("Avaliação não encontrada");

        if (review.UserId != userId)
            throw new ForbiddenException("Você não pode editar esta avaliação");

        review.Update(rating, comment);

        await _repository.UpdateAsync(review);
    }

    public async Task DeleteAsync(int reviewId, int userId)
    {
        var review = await _repository.GetByIdAsync(reviewId);

        if (review == null)
            throw new NotFoundException("Avaliação não encontrada");

        if (review.UserId != userId)
            throw new ForbiddenException("Você não pode deletar esta avaliação");

        await _repository.DeleteAsync(reviewId);
    }


}