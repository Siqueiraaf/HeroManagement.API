namespace HeroManagement.Application;

public record CriarHeroiDto(
    string Nome,
    string NomeHeroi,
    DateTime DataNascimento,
    double Altura,
    double Peso,
    List<int> SuperpoderesIds
);
