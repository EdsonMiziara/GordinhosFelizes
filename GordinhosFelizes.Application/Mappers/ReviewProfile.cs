using AutoMapper;
using GordinhosFelizes.Application.DTOs.Create;
using GordinhosFelizes.Application.DTOs.Update;

namespace GordinhosFelizes.Application.Mappers;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<CreateReviewDto, Review>();
        CreateMap<Review, UpdateReviewDto>();
    }
}
