using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class ConsultaService : BaseService<Consulta>, IConsultaService
    {
        private readonly IRepository<Consulta> _consultaRepository;

        public ConsultaService(IRepository<Consulta> consultaRepository) : base(consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        // Métodos específicos de Consulta
        public async Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId)
        {
            var all = await _consultaRepository.GetAllAsync();
            return all.Where(c => c.PacienteId == pacienteId);
        }

        public async Task<IEnumerable<Consulta>> GetByMedicoIdAsync(int medicoId)
        {
            var all = await _consultaRepository.GetAllAsync();
            return all.Where(c => c.MedicoId == medicoId);
        }
    }
}
