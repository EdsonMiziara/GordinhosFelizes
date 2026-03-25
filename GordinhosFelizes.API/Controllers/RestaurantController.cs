using AutoMapper;
using GordinhosFelizes.API.Models;
using GordinhosFelizes.Application.DTOs.Create;
using GordinhosFelizes.Application.DTOs.Response;
using GordinhosFelizes.Application.DTOs.Update;
using GordinhosFelizes.Application.Mappers;
using GordinhosFelizes.Application.Services;
using GordinhosFelizes.Domain.Enums;
using GordinhosFelizes.Domain.Interfaces;
using GordinhosFelizes.Domain.Models;
using GordinhosFelizes.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _service;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;



    public RestaurantsController( RestaurantService service, ICurrentUserService currentUser, IMapper mapper)
    {
        _service = service;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        var response = _mapper.Map<List<RestaurantResponseDto>>(data);
        return Ok(response);
    }


    [HttpPost]
    [Authorize(Roles = nameof(Roles.admin))]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
    {
        var userId = _currentUser.UserId;

        var restaurant = new Restaurant(dto.Name, dto.Description, userId);

        await _service.CreateAsync(restaurant);

        var response = _mapper.Map<RestaurantResponseDto>(restaurant);

        return Created("", ApiResponse<RestaurantResponseDto>.Ok(response));
    }

    [HttpPut]
    [Authorize(Roles = nameof(Roles.admin))]
    public async Task<IActionResult> Update(int id, UpdateRestaurantDto dto)
    {
        var restaurant = await _service.GetByIdAsync(id);
        if (restaurant == null)
            return NotFound(ApiResponse<string>.Fail("Restaurant not found"));
        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        await _service.UpdateAsync(restaurant);
        var response = _mapper.Map<RestaurantResponseDto>(restaurant);
        return Ok(ApiResponse<RestaurantResponseDto>.Ok(response));
    }

    [HttpGet("ranking")]
    public async Task<IActionResult> GetRanking()
    {
        var response = await _service.GetRankingAsync();
        return Ok(ApiResponse<List<RestaurantRanking>>.Ok(response));
    }


}


