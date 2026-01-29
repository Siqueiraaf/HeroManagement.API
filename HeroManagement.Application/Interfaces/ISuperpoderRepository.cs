using HeroManagement.Domain;

namespace HeroManagement.Application;

public interface ISuperpoderRepository
{
    Task<IEnumerable<Superpoderes>> ObterTodosSuperpoderesAsync();
}
