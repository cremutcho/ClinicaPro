using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Adicionar se necessário, mas geralmente já é incluído

namespace ClinicaPro.Infrastructure.Repositories
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(ClinicaDbContext context) : base(context)
        {
        }

        // NOVO: Sobrescrevemos GetAllAsync para incluir a Especialidade (necessário para a tela Index).
        public override async Task<IEnumerable<Medico>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.Especialidade) // <<--- CORREÇÃO APLICADA!
                .ToListAsync();
        }

        // Sobrescrevemos o método GetByIdAsync da classe base para incluir a Especialidade (necessário para a tela Details).
        public override async Task<Medico?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id);
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