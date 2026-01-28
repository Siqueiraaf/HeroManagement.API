using HeroManagement.Domain;

namespace HeroManagement.Application;

public interface IHeroiService
{
    Task<int> CriarHeroiAsync(CriarHeroiDto dto);
    Task<IEnumerable<Heroi>> ObterTodosHeroisAsync();
    Task<Heroi?> ObterHeroiPorIdAsync(int id);
    Task<bool> AtualizarHeroiAsync(int id, AtualizarHeroiDto dto);
    Task<bool> RemoverHeroiAsync(int id);
}