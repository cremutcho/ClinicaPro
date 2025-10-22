using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    // O Controller passa a entidade Consulta completa.
    public record CriarConsultaCommand : IRequest<Unit> 
    {
        public required Consulta Consulta { get; init; }
    }
}