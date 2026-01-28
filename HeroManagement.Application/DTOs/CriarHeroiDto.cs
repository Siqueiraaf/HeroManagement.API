namespace HeroManagement.Application;

public class CriarHeroiDto
{
    public string Nome { get; set; } = null!;
    public string NomeHeroi { get; set; } = null!;
    public DateTime DataNascimento { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }

    public List<int> SuperpoderesIds { get; set; } = new();
}
