using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    // Query para buscar um convênio pelo Id
    public record GetConvenioMedicoByIdQuery(Guid Id) 
        : IRequest<ClinicaPro.Core.Entities.ConvenioMedico>;
}
