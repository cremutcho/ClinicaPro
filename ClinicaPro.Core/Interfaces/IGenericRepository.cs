// ClinicaPro.Core/Interfaces/IGenericRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    // T é a entidade (ex: Medico, Paciente, Especialidade)
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id); // Adicionado para substituir MedicoExists()
    }
}