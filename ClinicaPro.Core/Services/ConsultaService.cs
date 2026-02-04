using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IRepository<Consulta, Guid> _consultaRepository;

        public ConsultaService(IRepository<Consulta, Guid> consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Consulta> CriarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorario(consulta.MedicoId, consulta.DataHora);

            if (conflito)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta neste horário.");

            await _consultaRepository.AddAsync(consulta);
            return consulta;
        }

        public async Task<Consulta> AtualizarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorario(consulta.MedicoId, consulta.DataHora);

            if (conflito)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta neste horário.");

            await _consultaRepository.UpdateAsync(consulta);
            return consulta;
        }

        public async Task ExcluirAsync(Guid id)
        {
            await _consultaRepository.DeleteAsync(id);
        }

        public async Task<Consulta?> BuscarPorIdAsync(Guid id)
        {
            return await _consultaRepository.GetByIdAsync(id);
        }

        public async Task<List<Consulta>> ListarAsync()
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.ToList();
        }

        public async Task<bool> VerificaConflitoHorario(int medicoId, DateTime dataHora)
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.Any(c => c.MedicoId == medicoId && c.DataHora == dataHora);
        }
    }
}
