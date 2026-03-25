namespace GordinhosFelizes.Domain.Interface;

public interface IReview<T>
{
    void AddReview(int userId, int grade, string comment);
    double Media { get; }
}
