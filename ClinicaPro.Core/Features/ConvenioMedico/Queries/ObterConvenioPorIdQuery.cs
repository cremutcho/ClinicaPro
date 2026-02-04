using MediatR;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    public record ObterConvenioPorIdQuery : IRequest<ClinicaPro.Core.Entities.ConvenioMedico?>
    {
        public int Id { get; init; }
    }
}
