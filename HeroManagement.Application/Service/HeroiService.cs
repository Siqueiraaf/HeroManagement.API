using HeroManagement.Domain;

namespace HeroManagement.Application;

public class HeroiService : IHeroiService
{
    private readonly IHeroiRepository _repository;

    public HeroiService(IHeroiRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CriarHeroiAsync(CriarHeroiDto dto)
    {
        var heroi = new Heroi(
            dto.Nome,
            dto.NomeHeroi,
            dto.DataNascimento,
            dto.Altura,
            dto.Peso
        );

        foreach (var superpoderId in dto.SuperpoderesIds)
        {
            heroi.AdicionarSuperpoder(superpoderId);
        }

        await _repository.AdicionarHeroiAsync(heroi);
        await _repository.SalvarHeroiAsync();

        return heroi.Id;
    }

    public async Task<IEnumerable<Heroi>> ObterTodosHeroisAsync()
    {
        return await _repository.ObterTodosHeroisAsync();
    }

    public async Task<Heroi?> ObterHeroiPorIdAsync(int id)
    {
        return await _repository.ObterHeroiPorIdAsync(id);
    }

    public async Task AtualizarHeroiAsync(int id, CriarHeroiDto dto)
    {
        var heroi = await _repository.ObterHeroiPorIdAsync(id);
        if (heroi is null)
            throw new Exception("Herói não encontrado");

        await _repository.AtualizarHeroiAsync(heroi);
        await _repository.SalvarHeroiAsync();
    }

    public async Task RemoverHeroiAsync(int id)
    {
        var heroi = await _repository.ObterHeroiPorIdAsync(id);
        if (heroi is null)
            throw new Exception("Herói não encontrado");

        await _repository.RemoverHeroiAsync(heroi);
        await _repository.SalvarHeroiAsync();
    }
}
