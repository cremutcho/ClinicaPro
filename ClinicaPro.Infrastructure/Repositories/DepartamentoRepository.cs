using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPro.Infrastructure.Repositories
{
    // DepartamentoRepository herda de Repository<Departamento> (que implementa IRepository<Departamento>) 
    // e implementa a interface IDepartamentoRepository para métodos específicos.
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        private readonly ClinicaDbContext _dbContext;

        // Construtor que recebe o DbContext
        public DepartamentoRepository(ClinicaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Implementação do método específico definido em IDepartamentoRepository
        public async Task<Departamento?> GetByNomeAsync(string nome)
        {
            return await _dbContext.Departamentos
                                   .Where(d => d.Nome == nome)
                                   .FirstOrDefaultAsync();
        }

        // Observação: Os métodos CRUD básicos (GetByIdAsync, GetAllAsync, AddAsync, etc.) 
        // já são herdados e implementados pela classe base Repository<T>.
    }
}