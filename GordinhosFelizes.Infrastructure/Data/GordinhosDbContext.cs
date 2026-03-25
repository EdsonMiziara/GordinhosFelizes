using GordinhosFelizes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GordinhosFelizes.Infrastructure.Data;

public class GordinhosDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public GordinhosDbContext(DbContextOptions<GordinhosDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
    .Property(u => u.Role)
    .HasConversion<string>();

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });


        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("restaurants");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("reviews");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });
    }


}
