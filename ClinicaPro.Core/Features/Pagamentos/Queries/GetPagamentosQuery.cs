using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Pagamentos.Queries
{
    public class GetPagamentosQuery : IRequest<List<Pagamento>>
    {
    }
}
