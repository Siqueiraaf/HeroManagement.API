using HeroManagement.Domain;

namespace HeroManagement.Application;

public class HeroiService(IHeroiRepository repository) : IHeroiService
{
    private readonly IHeroiRepository _repository = repository;

    public async Task<int> CriarHeroiAsync(CriarHeroiDto dto)
    {
        var heroiExistente = await _repository.ObterHeroiPorNomeHeroiAsync(dto.NomeHeroi);
        if (heroiExistente != null)
            throw new InvalidOperationException("O nome do herói já está em uso por outro super-herói.");

        if (dto.SuperpoderesIds == null || dto.SuperpoderesIds.Count == 0)
            throw new InvalidOperationException("O herói deve ter pelo menos um superpoder.");

        var heroi = new Heroi(
            dto.Nome,
            dto.NomeHeroi,
            dto.DataNascimento,
            dto.Altura,
            dto.Peso
        );

        foreach (var superpoderId in dto.SuperpoderesIds)
            heroi.AdicionarSuperpoder(superpoderId);

        await _repository.AdicionarHeroiAsync(heroi);
        await _repository.SalvarHeroiAsync();

        return heroi.Id;
    }


    public async Task<IEnumerable<Heroi>> ObterTodosHeroisAsync()
    {
        var herois = await _repository.ObterTodosHeroisAsync()
            ?? throw new NotFoundException("Nenhum herói cadastrado.");

        return herois;
    }

    public async Task<Heroi?> ObterHeroiPorIdAsync(int id)
    {
        var heroi = await _repository.ObterHeroiPorIdAsync(id)
            ?? throw new NotFoundException("Herói não encontrado");

        return heroi;
    }

    public async Task<bool> AtualizarHeroiAsync(int id, AtualizarHeroiDto dto)
    {
        var heroi = await _repository.ObterHeroiPorIdAsync(id)
            ?? throw new NotFoundException("Herói não encontrado");

        if (!string.IsNullOrWhiteSpace(dto.NomeHeroi))
        {
            var heroiExistente = await _repository.ObterHeroiPorNomeHeroiAsync(dto.NomeHeroi);
            if (heroiExistente != null && heroiExistente.Id != id)
            {
                return false;
            }
        }

        var superpoderesIdsAtuais = heroi.HeroisSuperpoderes
            .Select(hs => hs.SuperpoderId).ToList();

        heroi.Atualizar(
            dto.Nome ?? heroi.Nome,
            dto.NomeHeroi ?? heroi.NomeHeroi,
            dto.DataNascimento ?? heroi.DataNascimento,
            dto.Altura ?? heroi.Altura,
            dto.Peso ?? heroi.Peso,
            dto.SuperpoderesIds ?? superpoderesIdsAtuais
        );

        await _repository.AtualizarHeroiAsync(heroi);
        await _repository.SalvarHeroiAsync();

        return true;
    }

    public async Task<bool> RemoverHeroiAsync(int id)
    {
        var heroi = await _repository.ObterHeroiPorIdAsync(id)
            ?? throw new Exception("Herói não encontrado");

        await _repository.RemoverHeroiAsync(heroi);
        await _repository.SalvarHeroiAsync();
        return true;
    }
}
