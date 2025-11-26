using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Queries
{
    public class GetAllContasPagarQuery : IRequest<IEnumerable<ContaPagar>>
    {
        // Essa query não precisa de parâmetros, pois busca todas as contas a pagar
    }
}
