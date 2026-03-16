namespace GordinhosFelizes.Models.Interface;

public interface IReview<T>
{
    void AddReview(int pessoaId, int nota);
    double Media { get; }
}
