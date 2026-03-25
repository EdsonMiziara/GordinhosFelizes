using GordinhosFelizes.Domain.Exceptions;
using GordinhosFelizes.Domain.Interfaces;
using GordinhosFelizes.Domain.Models;

public class RestaurantService
{
    private readonly IRestaurantRepository _repository;

    public RestaurantService(IRestaurantRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Restaurant restaurant)
    {
        if (string.IsNullOrWhiteSpace(restaurant.Name))
            throw new BusinessException("Nome é obrigatório");

        await _repository.AddAsync(restaurant);
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        if (!data.Any())
            throw new NotFoundException("Nenhum restaurante encontrado");

        return data;
    }

    public async Task<List<RestaurantRanking>> GetRankingAsync()
    {
        return await _repository.GetTopRatedAsync();
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        if (string.IsNullOrWhiteSpace(restaurant.Name))
            throw new BusinessException("Nome é obrigatório");
        if (string.IsNullOrWhiteSpace(restaurant.Description))
            throw new BusinessException("Descrição é obrigatória");
        await _repository.UpdateAsync(restaurant);
    }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
    }
}