// ClinicaPro.Core/Features/Pacientes/Commands/AtualizarPacienteCommand.cs
using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    public record AtualizarPacienteCommand : IRequest<Unit>
    {
        public Paciente Paciente { get; set; } = default!;
    }
}