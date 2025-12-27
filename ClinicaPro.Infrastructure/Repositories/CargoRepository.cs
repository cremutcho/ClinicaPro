using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPro.Infrastructure.Repositories
{
    // CargoRepository herda de Repository<Cargo> (que implementa IRepository<Cargo>) 
    // e implementa a interface ICargoRepository para métodos específicos.
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        private readonly ClinicaDbContext _dbContext;

        // Construtor que recebe o DbContext
        public CargoRepository(ClinicaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Implementação do método específico definido em ICargoRepository
        public async Task<Cargo?> GetByNomeAsync(string nome)
        {
            return await _dbContext.Cargos
                                   .Where(c => c.Nome == nome)
                                   .FirstOrDefaultAsync();
        }

        // Observação: Os métodos CRUD básicos (GetByIdAsync, GetAllAsync, AddAsync, etc.) 
        // já são herdados e implementados pela classe base Repository<T>.
    }
}