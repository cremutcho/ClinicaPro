using MediatR;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public class DeletarConvenioMedicoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeletarConvenioMedicoCommand(Guid id)
        {
            Id = id;
        }
    }
}
