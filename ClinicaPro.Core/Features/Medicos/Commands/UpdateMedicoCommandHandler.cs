using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public class UpdateMedicoCommandHandler 
        : IRequestHandler<UpdateMedicoCommand, Unit>
    {
        private readonly IMedicoService _medicoService;

        public UpdateMedicoCommandHandler(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        public async Task<Unit> Handle(UpdateMedicoCommand request, CancellationToken cancellationToken)
        {
            await _medicoService.AtualizarAsync(request.Medico);
            return Unit.Value;
        }
    }
}
