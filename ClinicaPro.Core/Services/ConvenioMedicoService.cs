using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;
using ClinicaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class ConvenioMedicoService : IConvenioMedicoService
    {
        private readonly IConvenioMedicoRepository _repository;

        public ConvenioMedicoService(IConvenioMedicoRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<ConvenioMedico> CriarAsync(ConvenioMedico convenio)
        {
            if (convenio == null)
                throw new BusinessException("Convênio inválido.");

            var nomeNormalizado = convenio.Nome?.Trim();

            if (string.IsNullOrWhiteSpace(nomeNormalizado))
                throw new BusinessException("O nome do convênio é obrigatório.");

            if (await _repository.ExistsByNameAsync(nomeNormalizado))
                throw new BusinessException("Já existe um convênio com esse nome.");

            convenio.Nome = nomeNormalizado;

            return await _repository.AddAsync(convenio);
        }

        // =========================
        // READ
        // =========================
        public async Task<List<ConvenioMedico>> ListarAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ConvenioMedico?> BuscarPorIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<ConvenioMedico> AtualizarAsync(ConvenioMedico convenio)
        {
            if (convenio == null)
                throw new BusinessException("Convênio inválido.");

            var nomeNormalizado = convenio.Nome?.Trim();

            if (string.IsNullOrWhiteSpace(nomeNormalizado))
                throw new BusinessException("O nome do convênio é obrigatório.");

            if (await _repository.ExistsByNameExceptIdAsync(nomeNormalizado, convenio.Id))
                throw new BusinessException("Já existe outro convênio com esse nome.");

            convenio.Nome = nomeNormalizado;

            return await _repository.UpdateAsync(convenio);
        }

        // =========================
        // DELETE
        // =========================
        public async Task<bool> DeletarAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
