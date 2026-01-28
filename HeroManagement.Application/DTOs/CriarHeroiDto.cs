namespace HeroManagement.Application;

public record CriarHeroiDto(
    string Nome,
    string NomeHeroi,
    DateTime DataNascimento,
    float Altura,
    float Peso,
    List<int> SuperpoderesIds
);
