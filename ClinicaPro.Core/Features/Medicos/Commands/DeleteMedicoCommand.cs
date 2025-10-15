// ClinicaPro.Core/Features/Medicos/Commands/DeleteMedicoCommand.cs
using MediatR;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    // O command de exclusão precisa apenas do ID.
    // Ele retorna Unit, indicando que a operação foi concluída.
    public record DeleteMedicoCommand(int Id) : IRequest<Unit>;
}