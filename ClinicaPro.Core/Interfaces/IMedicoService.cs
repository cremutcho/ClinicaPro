using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IMedicoService : IBaseService<Medico>
    {
        Task<IEnumerable<Medico>> GetByEspecialidadeAsync(string especialidade);
    }
}
