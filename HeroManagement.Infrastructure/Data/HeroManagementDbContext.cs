using HeroManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HeroManagement.Infrastructure;

public class HeroManagementDbContext : DbContext
{
    public HeroManagementDbContext(DbContextOptions<HeroManagementDbContext> options)
        : base(options) { }

    public DbSet<Heroi> Herois => Set<Heroi>();
    public DbSet<Superpoderes> Superpoderes => Set<Superpoderes>();
    public DbSet<HeroiSuperpoder> HeroisSuperpoderes => Set<HeroiSuperpoder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Heroi>(entity =>
        {
            entity.ToTable("Herois");
            entity.HasKey(h => h.Id);
        });

        modelBuilder.Entity<Superpoderes>(entity =>
        {
            entity.ToTable("Superpoderes");
            entity.HasKey(s => s.Id);
        });

        modelBuilder.Entity<HeroiSuperpoder>(entity =>
        {
            entity.ToTable("HeroisSuperpoderes");
            entity.HasKey(hs => new { hs.HeroiId, hs.SuperpoderId });
        });
    }
}
