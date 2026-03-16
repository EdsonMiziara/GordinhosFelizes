namespace GordinhosFelizes.Models;

public class Review
{
    public Review(int nota)
    {
        Nota = Math.Clamp(nota, 1, 5);
    }
    public int Nota { get; set; }
    public int? PessoaId { get; set; }
    public int Id { get; set; }

    public static Review Parse(string texto)
    {
        int nota = int.Parse(texto);
        return new Review(nota);
    }
}


