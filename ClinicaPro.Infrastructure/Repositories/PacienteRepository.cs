using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Paciente>> GetByNomeAsync(string nome)
        {
            return await _dbSet
                .Where(p => p.Nome.Contains(nome))
                .ToListAsync();
        }

        // Usa "new" para esconder o método da base
        public new async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // nunca retorna null
        }
    }
}
