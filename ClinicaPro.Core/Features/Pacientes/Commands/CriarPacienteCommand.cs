// ClinicaPro.Core/Features/Pacientes/Commands/CriarPacienteCommand.cs
using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    // O Command recebe a entidade Paciente completa.
    // Ele retorna a entidade Paciente criada (ou um DTO, mas vamos manter simples).
    public record CriarPacienteCommand : IRequest<Paciente>
    {
        public Paciente Paciente { get; set; } = default!;
    }
}