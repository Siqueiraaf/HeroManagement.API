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

    public Task AtualizarHeroiAsync(Heroi heroi)
    {
        _context.Herois.Update(heroi);
        return Task.CompletedTask;
    }

    public Task RemoverHeroiAsync(Heroi heroi)
    {
        _context.Herois.Remove(heroi);
        return Task.CompletedTask;
    }

    public async Task SalvarHeroiAsync()
    {
        await _context.SaveChangesAsync();
    }
        
}