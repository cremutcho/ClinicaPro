// ClinicaPro.Core/Features/Pacientes/Queries/ObterPacientePorIdQueryHandler.cs
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Queries
{
    public class ObterPacientePorIdQueryHandler : IRequestHandler<ObterPacientePorIdQuery, Paciente?>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public ObterPacientePorIdQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Paciente?> Handle(ObterPacientePorIdQuery request, CancellationToken cancellationToken)
        {
            // Usa o método genérico GetByIdAsync() do seu repositório
            return await _pacienteRepository.GetByIdAsync(request.Id);
        }
    }
}