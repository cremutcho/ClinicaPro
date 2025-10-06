using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        Task<Medico?> GetByCRMAsync(string crm);
        Task<IEnumerable<Medico>> GetByEspecialidadeAsync(int especialidadeId);
    }
}
