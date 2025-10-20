// ClinicaPro.Core/Features/Pacientes/Commands/AtualizarPacienteCommandHandler.cs
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    public class AtualizarPacienteCommandHandler : IRequestHandler<AtualizarPacienteCommand, Unit>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public AtualizarPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Unit> Handle(AtualizarPacienteCommand request, CancellationToken cancellationToken)
        {
            await _pacienteRepository.UpdateAsync(request.Paciente);
            
            return Unit.Value;
        }
    }
}