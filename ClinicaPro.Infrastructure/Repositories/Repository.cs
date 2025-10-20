using ClinicaPro.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClinicaPro.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Necessário para .AnyAsync

namespace ClinicaPro.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ClinicaDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ClinicaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        // ✅ ADICIONADO: Implementação do ExistsAsync para satisfazer IRepository<T>
        public async Task<bool> ExistsAsync(int id)
        {
            // Usa o método genérico do EF Core para verificar a propriedade 'Id'
            return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
        }
    }
}