using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente, int>
    {
        Task<Paciente?> GetByCPFAsync(string cpf);

        Task<IEnumerable<Paciente>> GetByNomeAsync(string nome);

        // 🔥 Oculta o método da interface base, se necessário
        new Task<IEnumerable<Paciente>> GetAllAsync();
    }
}
