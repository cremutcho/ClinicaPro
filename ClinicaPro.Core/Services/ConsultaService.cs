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
        private readonly IRepository<Consulta, int> _consultaRepository;

        public ConsultaService(IRepository<Consulta, int> consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Consulta> CriarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorario(
                consulta.MedicoId,
                consulta.DataHora
            );

            if (conflito)
                throw new InvalidOperationException(
                    "Conflito de horário: O médico já tem uma consulta neste horário."
                );

            await _consultaRepository.AddAsync(consulta);
            return consulta;
        }

        // =========================
        // UPDATE (🔥 AQUI ESTÁ A CORREÇÃO)
        // =========================
        public async Task<Consulta> AtualizarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorario(
                consulta.MedicoId,
                consulta.DataHora,
                consulta.Id   // 👈 IGNORA A PRÓPRIA CONSULTA
            );

            if (conflito)
                throw new InvalidOperationException(
                    "Conflito de horário: O médico já tem uma consulta neste horário."
                );

            await _consultaRepository.UpdateAsync(consulta);
            return consulta;
        }

        public async Task ExcluirAsync(int id)
        {
            await _consultaRepository.DeleteAsync(id);
        }

        public async Task<Consulta?> BuscarPorIdAsync(int id)
        {
            return await _consultaRepository.GetByIdAsync(id);
        }

        public async Task<List<Consulta>> ListarAsync()
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.ToList();
        }

        // =========================
        // REGRA DE CONFLITO (FINAL)
        // =========================
        public async Task<bool> VerificaConflitoHorario(
            int medicoId,
            DateTime dataHora,
            int? consultaId = null
        )
        {
            var todas = await _consultaRepository.GetAllAsync();

            return todas.Any(c =>
                c.MedicoId == medicoId &&
                c.DataHora == dataHora &&
                (!consultaId.HasValue || c.Id != consultaId.Value)
            );
        }
    }
}
