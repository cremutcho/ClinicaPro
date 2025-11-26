using MediatR;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Commands
{
    public record DeleteContaReceberCommand(int Id) : IRequest<bool>;
}
