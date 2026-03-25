namespace GordinhosFelizes.Domain.Models;

public class RestaurantRanking
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
}
