using MediatR;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public record UpdateConvenioMedicoCommand(
        ClinicaPro.Core.Entities.ConvenioMedico ConvenioMedico
    ) : IRequest<Unit>;
}
