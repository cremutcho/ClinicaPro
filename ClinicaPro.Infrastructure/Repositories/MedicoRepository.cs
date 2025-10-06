using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(ClinicaDbContext context) : base(context)
        {
        }

        public async Task<Medico?> GetByCRMAsync(string crm)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.CRM == crm);
        }

        public async Task<IEnumerable<Medico>> GetByEspecialidadeAsync(int especialidadeId)
        {
            return await _dbSet
                .Where(m => m.EspecialidadeId == especialidadeId)
                .ToListAsync();
        }
    }
}
