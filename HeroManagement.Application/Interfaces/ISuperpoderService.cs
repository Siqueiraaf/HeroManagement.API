using HeroManagement.Domain;

namespace HeroManagement.Application;

public interface ISuperpoderService
{
    Task<IEnumerable<Superpoderes>> ObterTodosSuperpoderesAsync();
}
