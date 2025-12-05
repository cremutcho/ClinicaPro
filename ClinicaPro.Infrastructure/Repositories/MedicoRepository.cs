using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(ClinicaDbContext context) : base(context)
        {
        }

        // Inclui Especialidade ao listar todos os médicos (necessário para dropdown e Index)
        public override async Task<IEnumerable<Medico>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.Especialidade)
                .ToListAsync();
        }

        // Inclui Especialidade ao buscar por ID (necessário para Details e Edit)
        public override async Task<Medico?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Busca por CRM
        public async Task<Medico?> GetByCRMAsync(string crm)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.CRM == crm);
        }

        // Busca por especialidade
        public async Task<IEnumerable<Medico>> GetByEspecialidadeAsync(int especialidadeId)
        {
            return await _dbSet
                .Where(m => m.EspecialidadeId == especialidadeId)
                .Include(m => m.Especialidade) // ✅ Incluído para consistência
                .ToListAsync();
        }

        // ✅ Novo método garantido para dropdown funcionar perfeitamente
        Task<IEnumerable<Medico>> IMedicoRepository.GetAllAsync()
        {
            return GetAllAsync();
        }
    }
}
