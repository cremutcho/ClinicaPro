using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class ConsultaService : BaseService<Consulta>, IConsultaService
    {
        private readonly IRepository<Consulta> _consultaRepository;

        public ConsultaService(IRepository<Consulta> consultaRepository) : base(consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        // =====================================
        // ✅ Implementando todos os métodos da interface
        // =====================================

        // Criar consulta com verificação de conflito
        public async Task<Consulta> CriarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorarioAsync(consulta.MedicoId, consulta.DataHora);
            if (conflito)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta neste horário.");

            await _consultaRepository.AddAsync(consulta);
            return consulta;
        }

        // Atualizar consulta com verificação de conflito
        public async Task AtualizarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorarioAsync(consulta.MedicoId, consulta.DataHora, consulta.Id);
            if (conflito)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta neste horário.");

            await _consultaRepository.UpdateAsync(consulta);
        }

        // Excluir consulta
        public async Task ExcluirAsync(int id)
        {
            await _consultaRepository.DeleteAsync(id);
        }

        // Consultas por paciente
        public async Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId)
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.Where(c => c.PacienteId == pacienteId);
        }

        // Consultas por médico
        public async Task<IEnumerable<Consulta>> GetByMedicoIdAsync(int medicoId)
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.Where(c => c.MedicoId == medicoId);
        }

        // Verificar conflito de horário
        public async Task<bool> VerificaConflitoHorarioAsync(int medicoId, DateTime dataHora, int? consultaId = null)
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.Any(c => c.MedicoId == medicoId 
                               && c.DataHora == dataHora
                               && (!consultaId.HasValue || c.Id != consultaId.Value));
        }
    }
}
