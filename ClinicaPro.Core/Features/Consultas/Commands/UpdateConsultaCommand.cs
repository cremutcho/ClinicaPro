using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public record UpdateConsultaCommand : IRequest<Unit>
    {
        public Consulta Consulta { get; set; } = default!;
    }
}