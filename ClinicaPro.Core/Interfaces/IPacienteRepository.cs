using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente?> GetByCPFAsync(string cpf);

        // 🔥 Palavra-chave 'new' para ocultar o método da interface base
        new Task<IEnumerable<Paciente>> GetAllAsync();
    }
}
