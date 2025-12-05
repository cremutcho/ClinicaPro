using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Servicos.Queries
{
    public class ObterTodosServicosQuery : IRequest<IEnumerable<Servico>>
    {
    }
}
