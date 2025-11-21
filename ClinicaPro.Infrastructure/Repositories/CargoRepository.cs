using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly ClinicaDbContext _context;

        public CargoRepository(ClinicaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync() => await _context.Cargos.ToListAsync();

        public async Task<Cargo?> GetByIdAsync(int id) => await _context.Cargos.FindAsync(id);

        public async Task AddAsync(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cargo cargo)
        {
            _context.Cargos.Update(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cargo = await GetByIdAsync(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
