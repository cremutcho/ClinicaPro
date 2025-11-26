using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Queries
{
    public class GetAllContasReceberQuery : IRequest<IEnumerable<ContaReceber>>
    {
    }
}
