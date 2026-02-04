using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico, int>
    {
        Task<Medico?> GetByCRMAsync(string crm);
        Task<IEnumerable<Medico>> GetByEspecialidadeAsync(int especialidadeId);

        // ✅ Adicionado para o dropdown funcionar
        new Task<IEnumerable<Medico>> GetAllAsync();
    }
}
