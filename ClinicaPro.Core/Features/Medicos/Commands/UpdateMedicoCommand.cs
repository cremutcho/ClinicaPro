// ClinicaPro.Core/Features/Medicos/Commands/UpdateMedicoCommand.cs
using MediatR;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public record UpdateMedicoCommand : IRequest<Unit>
    {
        public int Id { get; init; } // Necessário para identificar quem será atualizado
        public required string Nome { get; init; }
        public required string CRM { get; init; }
        public int EspecialidadeId { get; init; }
        public required string Email { get; init; }
        public required string Telefone { get; init; }
    }
}