using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using GordinhosFelizes.Application.DTOs.Create;
using GordinhosFelizes.Application.Mappers;
using GordinhosFelizes.Application.Services;
using GordinhosFelizes.Domain.Interface;
using GordinhosFelizes.Domain.Interfaces;
using GordinhosFelizes.Domain.Models;
using GordinhosFelizes.Infrastructure.Data;
using GordinhosFelizes.Infrastructure.Middleware;
using GordinhosFelizes.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<ReviewService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateReviewDto>();

builder.Services.AddSingleton<IMapper>(sp =>
{
    var config = new MapperConfigurationExpression();
    config.AddMaps(typeof(RestaurantProfile).Assembly);
    config.AddMaps(typeof(ReviewProfile).Assembly);

    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

    var mapperConfig = new MapperConfiguration(config, loggerFactory);

    return new Mapper(mapperConfig);
});

builder.Services.AddDbContext<GordinhosDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))
    ));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var key = builder.Configuration["Jwt:Key"]
          ?? throw new Exception("JWT Key não configurada");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        )
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Digite: Bearer SEU_TOKEN"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors(policy =>
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

