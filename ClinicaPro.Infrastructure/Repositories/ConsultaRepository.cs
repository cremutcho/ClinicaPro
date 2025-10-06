using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(ClinicaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Consulta>> GetByMedicoAsync(int medicoId)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Where(c => c.MedicoId == medicoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> GetByPacienteAsync(int pacienteId)
        {
            return await _dbSet
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }

        public async Task<bool> VerificaConflitoHorario(int medicoId, DateTime dataHora)
        {
            return await _dbSet.AnyAsync(c => c.MedicoId == medicoId && c.DataHora == dataHora);
        }
    }
}
