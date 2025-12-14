using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IPacienteService
    {
        Task<Paciente> CriarAsync(Paciente paciente);
        Task<IEnumerable<Paciente>> ObterTodosAsync();
        Task<Paciente?> ObterPorIdAsync(int id);
        Task AtualizarPacienteAsync(Paciente paciente);
        Task ExcluirPacienteAsync(int id);
    }
}
