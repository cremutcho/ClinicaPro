using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> GetAllAsync();
        Task<Cargo?> GetByIdAsync(int id);
        Task AddAsync(Cargo cargo);
        Task UpdateAsync(Cargo cargo);
        Task DeleteAsync(int id);
    }
}
