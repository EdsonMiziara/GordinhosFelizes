namespace GordinhosFelizes.Application.DTOs.Create;

public class CreateReviewDto
{
    public int RestaurantId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}