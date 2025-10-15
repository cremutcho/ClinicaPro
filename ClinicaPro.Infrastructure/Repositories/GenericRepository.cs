// ClinicaPro.Infrastructure/Repositories/GenericRepository.cs
using ClinicaPro.Core.Interfaces; // Para IGenericRepository
using ClinicaPro.Infrastructure.Data; // Para ClinicaDbContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// O namespace deve ser o mesmo usado em EspecialidadeRepository.cs
namespace ClinicaPro.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ClinicaDbContext _context;
        protected readonly DbSet<T> _dbSet; // DbSet para operações genéricas

        public GenericRepository(ClinicaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            // Supondo que T tem uma propriedade Id (Entity Framework a preenche após SaveChanges)
            var idProperty = typeof(T).GetProperty("Id");
            return idProperty != null ? (int)idProperty.GetValue(entity)! : 0;
        }

        public async Task UpdateAsync(T entity)
        {
            // O Attach é necessário para que o EF Core rastreie a entidade
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            // Verifica a existência sem buscar a entidade completa (mais eficiente)
            var keyProperty = _context.Model.FindEntityType(typeof(T))!.FindPrimaryKey()!.Properties.First();
            return await _dbSet.AnyAsync(e => EF.Property<int>(e, keyProperty.Name) == id);
        }
    }
}