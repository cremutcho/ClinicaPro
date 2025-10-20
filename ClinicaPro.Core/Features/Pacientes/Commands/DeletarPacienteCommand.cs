// ClinicaPro.Core/Features/Pacientes/Commands/DeletarPacienteCommand.cs
using MediatR;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    public record DeletarPacienteCommand : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}