using GordinhosFelizes.Domain.Enums;

namespace GordinhosFelizes.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Roles Role { get; set; }
    public DateTime CreatedAt { get; }

    protected User()
    {

    }

    public User(string name, string email, string passwordHash, Roles role)
    {
        CreatedAt = DateTime.UtcNow;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
    public async Task AddReview(Restaurant restaurant, int grade)
    {
        // Lógica para adicionar uma avaliação a um restaurante
    }

}
