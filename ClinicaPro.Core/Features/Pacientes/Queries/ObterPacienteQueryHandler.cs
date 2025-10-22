using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Pacientes.Queries
{
    // O Handler aceita a ObterPacienteQuery e retorna Paciente? (Paciente ou nulo)
    public class ObterPacienteQueryHandler : IRequestHandler<ObterPacienteQuery, Paciente?>
    {
        private readonly IPacienteRepository _pacienteRepository;

        // Injeção de Dependência: Recebe o repositório
        public ObterPacienteQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Paciente?> Handle(ObterPacienteQuery request, CancellationToken cancellationToken)
        {
            // O Handler apenas delega a busca ao repositório usando o ID da Query
            return await _pacienteRepository.GetByIdAsync(request.Id);
        }
    }
}