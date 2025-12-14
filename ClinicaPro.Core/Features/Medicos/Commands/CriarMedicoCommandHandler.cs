using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public class CriarMedicoCommandHandler 
        : IRequestHandler<CriarMedicoCommand, Unit>
    {
        private readonly IMedicoService _medicoService;

        public CriarMedicoCommandHandler(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        public async Task<Unit> Handle(
            CriarMedicoCommand request,
            CancellationToken cancellationToken)
        {
            await _medicoService.CriarAsync(request.Medico);
            return Unit.Value;
        }
    }
}
