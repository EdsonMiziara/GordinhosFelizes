public class Review
{
    protected Review() { }

    public int Id { get; private set; }

    public int RestaurantId { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; }

    private int _rating;
    public int Rating
    {
        get => _rating;
        private set => _rating = Math.Clamp(value, 1, 5);
    }

    public string Comment { get; private set; }

    public Review(int restaurantId, int userId, int rating, string comment)
    {
        RestaurantId = restaurantId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(int rating, string comment)
    {
        Rating = rating;
        Comment = comment;
    }
}