using HeroManagement.Application;
using HeroManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HeroManagement.Infrastructure;

public class SuperpoderRepository : ISuperpoderRepository
{
    private readonly HeroManagementDbContext _context;

    public SuperpoderRepository(HeroManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Superpoderes>> ObterTodosSuperpoderesAsync()
    {
        return await _context.Superpoderes.ToListAsync();
    }
}
