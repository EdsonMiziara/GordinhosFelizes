using AutoMapper;
using GordinhosFelizes.Application.DTOs.Create;
using GordinhosFelizes.Application.DTOs.Response;
using GordinhosFelizes.Domain.Models;

namespace GordinhosFelizes.Application.Mappers;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantResponseDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
    }

}
