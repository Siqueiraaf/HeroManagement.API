namespace HeroManagement.Application;

public record AtualizarHeroiDto(
    int Id,
    string? Nome,
    string? NomeHeroi,
    DateTime? DataNascimento,
    double? Altura,
    double? Peso,
    List<int>? SuperpoderesIds
);
