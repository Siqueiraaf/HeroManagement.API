using HeroManagement.Domain;

namespace HeroManagement.Application;

public interface IHeroiService
{
    Task<int> CriarHeroiAsync(CriarHeroiDto dto);
    Task<IEnumerable<Heroi>> ObterTodosHeroisAsync();
    Task<Heroi?> ObterHeroiPorIdAsync(int id);
    Task AtualizarHeroiAsync(int id, CriarHeroiDto dto);
    Task RemoverHeroiAsync(int id);
}