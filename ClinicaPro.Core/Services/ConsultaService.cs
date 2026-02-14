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
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
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
                consulta.DataHora,
                null
            );

            if (conflito)
                throw new InvalidOperationException(
                    "Já existe uma consulta para este médico nesse horário."
                );

            await _consultaRepository.AddAsync(consulta);

            return consulta;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Consulta> AtualizarAsync(Consulta consulta)
        {
            var conflito = await VerificaConflitoHorario(
                consulta.MedicoId,
                consulta.DataHora,
                consulta.Id
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
        // REGRA DE CONFLITO (AGORA OTIMIZADA)
        // =========================
        public async Task<bool> VerificaConflitoHorario(
            int medicoId,
            DateTime dataHora,
            int? consultaId
        )
        {
            return await _consultaRepository
                .ExisteConsultaNoHorarioAsync(
                    medicoId,
                    dataHora,
                    consultaId
                );
        }
    }
}
