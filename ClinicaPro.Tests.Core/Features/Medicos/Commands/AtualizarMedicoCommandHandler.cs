using MediatR;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Medicos.Commands // NAMESPACE CRÍTICO
{
    public class AtualizarMedicoCommandHandler : IRequestHandler<AtualizarMedicoCommand, Unit>
    {
        private readonly IMedicoRepository _medicoRepository;

        public AtualizarMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Unit> Handle(AtualizarMedicoCommand request, CancellationToken cancellationToken)
        {
            await _medicoRepository.UpdateAsync(request.Medico);
            return Unit.Value;
        }
    }
}