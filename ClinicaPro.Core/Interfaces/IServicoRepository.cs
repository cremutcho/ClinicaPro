using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IServicoRepository : IRepository<Servico>
    {
        Task<IEnumerable<Servico>> GetAllAsync();
        Task<Servico?> GetByIdAsync(int id);
    }
}
