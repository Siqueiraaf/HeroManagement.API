using HeroManagement.Application;
using HeroManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HeroManagement.Infrastructure;

public class HeroiRepository(HeroManagementDbContext context) : IHeroiRepository
{
    private readonly HeroManagementDbContext _context = context;

    public async Task AdicionarHeroiAsync(Heroi heroi)
    {
        await _context.Herois.AddAsync(heroi);
    }

    public async Task<Heroi?> ObterHeroiPorIdAsync(int id)
    {
        return await _context.Herois
        .Include(heroi => heroi.HeroisSuperpoderes)
        .FirstOrDefaultAsync(heroi => heroi.Id == id);
    }

    public async Task<IEnumerable<Heroi>> ObterTodosHeroisAsync()
    {
        return await _context.Herois
        .Include(heroi => heroi.HeroisSuperpoderes)
        .ToListAsync();
    }
    public async Task<Heroi?> ObterHeroiPorNomeHeroiAsync(string nomeHeroi)
    {
        return await _context.Herois
            .Include(heroi => heroi.HeroisSuperpoderes)
            .FirstOrDefaultAsync(heroi => heroi.NomeHeroi == nomeHeroi);
    }

    public async Task AtualizarHeroiAsync(Heroi heroi)
    {
        _context.Herois.Update(heroi);
    }

    public async Task RemoverHeroiAsync(Heroi heroi)
    {
         _context.Herois.Remove(heroi);
    }

    public async Task SalvarHeroiAsync()
    {
        await _context.SaveChangesAsync();
    }
}