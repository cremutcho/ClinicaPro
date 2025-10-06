using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConsultaService : IBaseService<Consulta>
    {
        // Aqui você pode adicionar métodos específicos de Consulta, por exemplo:
        Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId);
        Task<IEnumerable<Consulta>> GetByMedicoIdAsync(int medicoId);
    }
}
