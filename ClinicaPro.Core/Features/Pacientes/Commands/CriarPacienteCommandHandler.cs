// ClinicaPro.Core/Features/Pacientes/Commands/CriarPacienteCommandHandler.cs
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    public class CriarPacienteCommandHandler : IRequestHandler<CriarPacienteCommand, Paciente>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public CriarPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Paciente> Handle(CriarPacienteCommand request, CancellationToken cancellationToken)
        {
            // O handler simplesmente delega a operação de escrita para o repositório
            await _pacienteRepository.AddAsync(request.Paciente);
            
            return request.Paciente; // Retorna o objeto adicionado
        }
    }
}