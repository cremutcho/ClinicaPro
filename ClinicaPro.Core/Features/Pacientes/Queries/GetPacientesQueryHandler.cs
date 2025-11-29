using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Queries
{
    public class GetPacientesQueryHandler : IRequestHandler<GetPacientesQuery, List<Paciente>>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public GetPacientesQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<List<Paciente>> Handle(GetPacientesQuery request, CancellationToken cancellationToken)
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return pacientes.ToList(); // 🔥 converte IEnumerable → List
        }
    }
}
