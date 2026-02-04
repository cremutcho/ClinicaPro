using ClinicaPro.Core.Entities;
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
            return await _repository.AddAsync(convenio);
        }

        // =========================
        // READ
        // =========================
        public async Task<List<ConvenioMedico>> ListarAsync()
        {
            return await _repository.GetAllAsync(); // Retorno agora é List<ConvenioMedico>
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
