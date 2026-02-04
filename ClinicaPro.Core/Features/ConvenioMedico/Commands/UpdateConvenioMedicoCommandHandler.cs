using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public class UpdateConvenioMedicoCommandHandler
        : IRequestHandler<UpdateConvenioMedicoCommand, Unit>
    {
        private readonly IConvenioMedicoService _service;

        public UpdateConvenioMedicoCommandHandler(IConvenioMedicoService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(
            UpdateConvenioMedicoCommand request,
            CancellationToken cancellationToken)
        {
            await _service.AtualizarAsync(request.ConvenioMedico);

            return Unit.Value;
        }
    }
}
