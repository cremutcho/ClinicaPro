// ClinicaPro.Core/Features/Pacientes/Commands/DeletarPacienteCommandHandler.cs
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    public class DeletarPacienteCommandHandler : IRequestHandler<DeletarPacienteCommand, Unit>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public DeletarPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Unit> Handle(DeletarPacienteCommand request, CancellationToken cancellationToken)
        {
            await _pacienteRepository.DeleteAsync(request.Id);
            
            return Unit.Value;
        }
    }
}