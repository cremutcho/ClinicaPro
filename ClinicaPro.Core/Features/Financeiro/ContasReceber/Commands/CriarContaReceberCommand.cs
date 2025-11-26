using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Commands
{
    public record CriarContaReceberCommand(ContaReceber Conta) : IRequest<ContaReceber>;
}
