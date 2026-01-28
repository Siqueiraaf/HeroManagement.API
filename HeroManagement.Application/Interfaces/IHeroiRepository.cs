using HeroManagement.Domain;

namespace HeroManagement.Application;

public interface IHeroiRepository
{
    Task AdicionarHeroiAsync(Heroi heroi);
    Task<Heroi?> ObterHeroiPorIdAsync(int id);
    Task<IEnumerable<Heroi>> ObterTodosHeroisAsync();
    Task AtualizarHeroiAsync(Heroi heroi);
    Task RemoverHeroiAsync(Heroi heroi);
    Task SalvarHeroiAsync();
    Task<Heroi?> ObterHeroiPorNomeHeroiAsync(string nomeHeroi);
}
