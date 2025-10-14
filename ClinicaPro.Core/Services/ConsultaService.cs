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

        // Retorna todas as consultas de um paciente
        public async Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId)
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.Where(c => c.PacienteId == pacienteId);
        }

        // Retorna todas as consultas de um médico
        public async Task<IEnumerable<Consulta>> GetByMedicoIdAsync(int medicoId)
        {
            var todas = await _consultaRepository.GetAllAsync();
            return todas.Where(c => c.MedicoId == medicoId);
        }
    }
}
