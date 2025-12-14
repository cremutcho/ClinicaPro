using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IServicoService
    {
        Task<Servico> CriarAsync(Servico servico);
        Task<bool> AtualizarAsync(Servico servico);
        Task<bool> ExcluirAsync(int id);
        Task<IEnumerable<Servico>> ObterTodosAsync();
        Task<Servico?> ObterPorIdAsync(int id);
    }
}
