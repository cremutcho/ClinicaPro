using ClinicaPro.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClinicaPro.Infrastructure.Data;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class
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

        public virtual async Task<T?> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // =========================
        // 🔥 UPDATE CORRIGIDO
        // =========================
        public async Task UpdateAsync(T entity)
        {
            // 🔹 Obtém o Id da entidade dinamicamente
            var key = _context.Model
                .FindEntityType(typeof(T))!
                .FindPrimaryKey()!
                .Properties
                .First();

            var keyValue = typeof(T)
                .GetProperty(key.Name)!
                .GetValue(entity);

            // 🔹 Verifica se já existe entidade rastreada com o mesmo Id
            var trackedEntity = _context.ChangeTracker
                .Entries<T>()
                .FirstOrDefault(e =>
                    e.Property(key.Name).CurrentValue!.Equals(keyValue)
                );

            if (trackedEntity != null)
            {
                // 🧹 Remove a entidade antiga do ChangeTracker
                trackedEntity.State = EntityState.Detached;
            }

            // ✅ Agora é seguro atualizar
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }
    }
}
