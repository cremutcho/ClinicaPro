using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Necessário para .Where
using System; // Necessário para DateTime

namespace ClinicaPro.Infrastructure.Repositories
{
    // A classe herda de Repository<Consulta> e implementa IConsultaRepository
    public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(ClinicaDbContext context) : base(context)
        {
        }

        // ✅ 1. Método Sobrescrito para carregar Medico e Paciente (CORREÇÃO DE NRE)
        // Este é usado pela ObterTodasConsultasQuery.
        public override async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .ToListAsync();
        }

        // ✅ 2. Implementa GetByMedicoAsync (Método que estava na sua versão anterior)
        public async Task<IEnumerable<Consulta>> GetByMedicoAsync(int medicoId)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Where(c => c.MedicoId == medicoId)
                .ToListAsync();
        }

        // ✅ 3. Implementa GetByPacienteAsync (Método exigido pelo erro CS0535)
        public async Task<IEnumerable<Consulta>> GetByPacienteAsync(int pacienteId)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }

        // ✅ 4. Implementa VerificaConflitoHorario (Método exigido pelo erro CS0535)
        public async Task<bool> VerificaConflitoHorario(int medicoId, DateTime dataHora)
        {
            return await _dbSet.AnyAsync(c => c.MedicoId == medicoId && c.DataHora == dataHora);
        }
    }
}