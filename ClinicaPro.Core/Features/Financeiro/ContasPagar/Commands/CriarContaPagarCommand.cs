using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Commands
{
    public record CriarContaPagarCommand(ContaPagar Conta) : IRequest<ContaPagar>;
}
