using GordinhosFelizes.Domain.Interface;
using System.Xml;

namespace GordinhosFelizes.Domain.Models;

public class Restaurant : IReview<Restaurant>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedAt { get; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    protected Restaurant()
    {
        
    }

    public Restaurant(string name, string description, int createdById)
    {
        CreatedAt = DateTime.UtcNow;
        Name = name;
        Description = description;
        CreatedById = createdById;
    }


    public double Media
    {
        get
        {
            if (Reviews.Count == 0) return 0;
            return Reviews.Average(g => g.Rating);
        }
    }

    public void AddReview(int userId, int grade, string comment)
    {
        grade = Math.Min(Math.Max(grade, 1), 5);
        Reviews.Add(new Review(Id, grade, userId, comment));
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
