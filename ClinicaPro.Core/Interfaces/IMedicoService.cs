using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IMedicoService
    {
        Task CriarAsync(Medico medico);
        Task AtualizarAsync(Medico medico);
        Task<IEnumerable<Medico>> ObterTodosAsync();
        Task<Medico?> ObterPorIdAsync(int id);
        Task ExcluirAsync(int id);
    }
}
