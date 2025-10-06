using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class MedicoService : BaseService<Medico>, IMedicoService

    {
        private readonly IRepository<Medico> _medicoRepository;

        public MedicoService(IRepository<Medico> medicoRepository) : base(medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        // Métodos específicos de Médico
        public async Task<IEnumerable<Medico>> GetByEspecialidadeAsync(string especialidade)
        {
            var all = await _medicoRepository.GetAllAsync();
            return all.Where(m =>
                m.Especialidade != null &&
                m.Especialidade.Nome.Contains(especialidade, StringComparison.OrdinalIgnoreCase));
        }

    }
}
