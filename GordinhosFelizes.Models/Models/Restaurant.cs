using GordinhosFelizes.Models.Interface;

namespace GordinhosFelizes.Models.Models;

public class Restaurant : IReview<Restaurant>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public double Media => ();

    public void AddReview(int userId, int grade)
    {
        
    }
}
