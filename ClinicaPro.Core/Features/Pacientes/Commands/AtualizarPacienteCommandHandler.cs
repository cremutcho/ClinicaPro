using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    public class AtualizarPacienteCommandHandler 
        : IRequestHandler<AtualizarPacienteCommand, Unit>
    {
        private readonly IPacienteService _pacienteService;

        public AtualizarPacienteCommandHandler(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public async Task<Unit> Handle(
            AtualizarPacienteCommand request, 
            CancellationToken cancellationToken)
        {
            await _pacienteService.AtualizarPacienteAsync(request.Paciente);

            return Unit.Value;
        }
    }
}
