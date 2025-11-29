using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(ClinicaDbContext context) : base(context)
        {
        }

        public async Task<Paciente?> GetByCPFAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.CPF == cpf);
        }

        public async Task<List<Paciente>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // nunca retorna null
        }
    }
}
