using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        public ServicoRepository(ClinicaDbContext context)
            : base(context)
        {
        }

        // Substitui o método da base para retornar todos os serviços
        public override async Task<IEnumerable<Servico>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Substitui o método da base para retornar um serviço por ID
        public override async Task<Servico?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
