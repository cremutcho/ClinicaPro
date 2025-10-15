// ClinicaPro.Core/Features/Medicos/Commands/CriarMedicoCommand.cs
using MediatR;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    // O Command (a requisição de escrita) retornará o ID da nova entidade.
    public record CriarMedicoCommand : IRequest<int>
    {
        public string Nome { get; init; }
        public string CRM { get; init; }
        public int EspecialidadeId { get; init; }
        public string Email { get; init; }
        public string Telefone { get; init; }
    }
}