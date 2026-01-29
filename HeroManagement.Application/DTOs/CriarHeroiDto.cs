namespace HeroManagement.Application;

public class CriarHeroiDto
{
    public string Nome { get; init; }
    public string NomeHeroi { get; init; }
    public DateTime DataNascimento { get; init; }
    public float Altura { get; init; }
    public float Peso { get; init; }
    public List<int> SuperpoderesIds { get; init; }
}

