using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _repository;

        public ServicoService(IServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Servico> CriarAsync(Servico servico)
        {
            var existente = (await _repository.GetAllAsync())
                             .FirstOrDefault(s => s.Nome.ToLower() == servico.Nome.ToLower());

            if (existente != null)
                throw new BusinessException("Já existe um serviço cadastrado com este nome.");

            await _repository.AddAsync(servico);
            return servico;
        }

        public async Task<bool> AtualizarAsync(Servico servico)
        {
            var existente = (await _repository.GetAllAsync())
                             .FirstOrDefault(s => s.Nome.ToLower() == servico.Nome.ToLower() && s.Id != servico.Id);

            if (existente != null)
                throw new BusinessException("Já existe outro serviço cadastrado com este nome.");

            await _repository.UpdateAsync(servico);
            return true;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Servico>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Servico?> ObterPorIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
