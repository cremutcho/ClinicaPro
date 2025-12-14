using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _repository;

        public MedicoService(IMedicoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(Medico medico)
        {
            var existente = await _repository.GetByCRMAsync(medico.CRM);

            if (existente != null)
                throw new BusinessException("Já existe um médico cadastrado com este CRM.");

            await _repository.AddAsync(medico);
        }

        public async Task AtualizarAsync(Medico medico)
        {
            var existente = await _repository.GetByCRMAsync(medico.CRM);

            if (existente != null && existente.Id != medico.Id)
                throw new BusinessException("Já existe outro médico cadastrado com este CRM.");

            await _repository.UpdateAsync(medico);
        }

        public async Task<IEnumerable<Medico>> ObterTodosAsync()
            => await _repository.GetAllAsync();

        public async Task<Medico?> ObterPorIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task ExcluirAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
