using AutoMapper;
using GordinhosFelizes.API.Models;
using GordinhosFelizes.Application.DTOs.Create;
using GordinhosFelizes.Application.DTOs.Response;
using GordinhosFelizes.Application.DTOs.Update;
using GordinhosFelizes.Application.Mappers;
using GordinhosFelizes.Application.Services;
using GordinhosFelizes.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _service;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public ReviewController(ReviewService service, ICurrentUserService currentUser, IMapper mapper)
    {
        _service = service;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
    {
        var userId = _currentUser.UserId;

        var review = new Review(
            dto.RestaurantId,           
            userId,
            dto.Rating,
            dto.Comment
        );

        await _service.CreateAsync(review);

        var response = _mapper.Map<ReviewResponseDto>(review);

        return Created("", ApiResponse<ReviewResponseDto>.Ok(response));

    }

    [HttpGet("restaurant/{id}")]
    public async Task<IActionResult> GetByRestaurant(int id)
    {
        var data = await _service.GetByRestaurantAsync(id);
        var response = _mapper.Map<List<ReviewResponseDto>>(data);
        return Ok(ApiResponse<List<ReviewResponseDto>>.Ok(response));
    }

    [HttpGet("restaurant/{id}/average")]
    public async Task<IActionResult> GetAverage(int id)
    {
        var avg = await _service.GetAverageAsync(id);
        return Ok(ApiResponse<double>.Ok(avg));
    }

   [HttpPut("{id}")]
   [Authorize]
    public async Task<IActionResult> Update(int id, UpdateReviewDto dto)
    {
            var userId = _currentUser.UserId;

            await _service.UpdateAsync(
                id,
                userId,
                dto.Rating,
                dto.Comment
            );

            return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
            var userId = _currentUser.UserId;
            await _service.DeleteAsync(id, userId);
            return NoContent();
    }


}