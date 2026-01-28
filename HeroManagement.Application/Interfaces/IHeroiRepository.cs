using HeroManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroManagement.Application;

public interface IHeroiRepository
{
    Task AdicionarHeroiAsync(Heroi heroi);
    Task<Heroi?> ObterHeroiPorIdAsync(int id);
    Task<IEnumerable<Heroi>> ObterTodosHeroisAsync();
    Task AtualizarHeroiAsync(Heroi heroi);
    Task RemoverHeroiAsync(Heroi heroi);
    Task SalvarHeroiAsync();
}
