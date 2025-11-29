using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente?> GetByCPFAsync(string cpf);
        Task<IEnumerable<Paciente>> GetAllAsync();

    }
}
