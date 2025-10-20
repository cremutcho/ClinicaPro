using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        
        // ✅ ADICIONADO: Necessário para a lógica de concorrência e o ConsultaExisteQuery
        Task<bool> ExistsAsync(int id);
    }
}