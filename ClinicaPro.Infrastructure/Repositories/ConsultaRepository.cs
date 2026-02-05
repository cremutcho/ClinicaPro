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
    // A classe herda de Repository<Consulta, int> e implementa IConsultaRepository
    public class ConsultaRepository : Repository<Consulta, int>, IConsultaRepository
    {
        public ConsultaRepository(ClinicaDbContext context) : base(context)
        {
        }

        // ✅ 1. GetAllAsync – usado na listagem
        public override async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Servico)
                .Include(c => c.ConvenioMedico)
                .ToListAsync();
        }

        // ✅ 2. GetByIdAsync – ESSENCIAL para Details / Edit
        public override async Task<Consulta?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Servico)
                .Include(c => c.ConvenioMedico)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // ✅ 3. Consultas por médico
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

        // ✅ 4. Consultas por paciente
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

        // ✅ 5. Regra de conflito de horário
        public async Task<bool> VerificaConflitoHorario(int medicoId, DateTime dataHora)
        {
            return await _dbSet
                .AnyAsync(c => c.MedicoId == medicoId && c.DataHora == dataHora);
        }
    }
}
