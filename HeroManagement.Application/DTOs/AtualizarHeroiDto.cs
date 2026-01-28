namespace HeroManagement.Application;

public record AtualizarHeroiDto(
    int Id,
    string? Nome = default!,
    string? NomeHeroi = default!,
    DateTime? DataNascimento = null,
    float? Altura = default!,
    float? Peso = default!,
    List<int>? SuperpoderesIds = default!
);
