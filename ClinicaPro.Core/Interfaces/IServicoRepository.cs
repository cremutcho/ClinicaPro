using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IServicoRepository : IRepository<Servico>
    {
        // 🔥 'new' indica que estamos ocultando o método da interface base
        new Task<IEnumerable<Servico>> GetAllAsync();
        new Task<Servico?> GetByIdAsync(int id);
    }
}
