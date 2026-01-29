using HeroManagement.Domain;
using HeroManagement.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroManagement.Application
{
    public class SuperpoderService : ISuperpoderService
    {
        private readonly ISuperpoderRepository _repository;

        public SuperpoderService(ISuperpoderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Superpoderes>> ObterTodosSuperpoderesAsync()
        {
            return await _repository.ObterTodosSuperpoderesAsync();
        }
    }
}
