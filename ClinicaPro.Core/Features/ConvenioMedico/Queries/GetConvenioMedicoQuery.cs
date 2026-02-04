using MediatR;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    public record GetConvenioMedicoQuery 
        : IRequest<List<ClinicaPro.Core.Entities.ConvenioMedico>>;
}
