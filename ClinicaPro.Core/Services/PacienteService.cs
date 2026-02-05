using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Paciente> CriarAsync(Paciente paciente)
        {
            var existente = await _repository.GetByCPFAsync(paciente.CPF);

            if (existente != null)
                throw new BusinessException("Já existe um paciente cadastrado com este CPF.");

            await _repository.AddAsync(paciente);
            return paciente;
        }

        // =========================
        // READ
        // =========================
        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
            => await _repository.GetAllAsync();

        public async Task<Paciente?> ObterPorIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        // =========================
        // UPDATE (✅ SEM CONFLITO EF)
        // =========================
        public async Task AtualizarPacienteAsync(Paciente paciente)
        {
            var pacienteComMesmoCpf = await _repository.GetByCPFAsync(paciente.CPF);

            if (pacienteComMesmoCpf != null && pacienteComMesmoCpf.Id != paciente.Id)
                throw new BusinessException("Já existe um paciente cadastrado com este CPF.");

            // ✔️ delega para o Repository (que já está corrigido)
            await _repository.UpdateAsync(paciente);
        }

        // =========================
        // DELETE
        // =========================
        public async Task ExcluirPacienteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
