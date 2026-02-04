using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Services
{
    public class BaseService<T, TId> where T : class
    {
        protected readonly IRepository<T, TId> _repository;

        public BaseService(IRepository<T, TId> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T?> GetByIdAsync(TId id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(TId id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
