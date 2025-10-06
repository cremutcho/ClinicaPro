using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IPacienteService : IBaseService<Paciente>
    {
        // Métodos específicos de Paciente
        Task<IEnumerable<Paciente>> GetByNomeAsync(string nome);

        Task TesteConexaoAsync();
    }
}
