using MediatR;
using ConvenioEntity = ClinicaPro.Core.Entities.ConvenioMedico;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public record CriarConvenioMedicoCommand(
        ConvenioEntity ConvenioMedico
    ) : IRequest<ConvenioEntity>;
}
