using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class ConsultaRepository 
        : Repository<Consulta, int>, IConsultaRepository
    {
        public ConsultaRepository(ClinicaDbContext context) 
            : base(context)
        {
        }

        // =========================
        // LISTAGEM COMPLETA
        // =========================
        public override async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Servico)
                .Include(c => c.ConvenioMedico)
                .ToListAsync();
        }

        // =========================
        // BUSCAR POR ID
        // =========================
        public override async Task<Consulta?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Servico)
                .Include(c => c.ConvenioMedico)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // =========================
        // CONSULTAS POR MÉDICO
        // =========================
        public async Task<IEnumerable<Consulta>> GetByMedicoAsync(int medicoId)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Servico)
                .Include(c => c.ConvenioMedico)
                .Where(c => c.MedicoId == medicoId)
                .ToListAsync();
        }

        // =========================
        // CONSULTAS POR PACIENTE
        // =========================
        public async Task<IEnumerable<Consulta>> GetByPacienteAsync(int pacienteId)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Servico)
                .Include(c => c.ConvenioMedico)
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }

        // =========================
        // VERIFICAÇÃO OTIMIZADA NO BANCO
        // =========================
        public async Task<bool> ExisteConsultaNoHorarioAsync(
            int medicoId,
            DateTime dataHora,
            int? consultaId
        )
        {
            return await _dbSet.AnyAsync(c =>
                c.MedicoId == medicoId &&
                c.DataHora == dataHora &&
                (!consultaId.HasValue || c.Id != consultaId.Value)
            );
        }
    }
}
