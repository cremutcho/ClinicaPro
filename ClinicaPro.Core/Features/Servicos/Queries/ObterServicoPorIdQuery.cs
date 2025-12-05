using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Servicos.Queries
{
    public class ObterServicoPorIdQuery : IRequest<Servico?>
    {
        public int Id { get; set; }
    }
}
