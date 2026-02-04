// ClinicaPro.Core/Features/Convenios/Commands/DeletarConvenioCommandHandler.cs
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public class DeletarConvenioMedicoCommandHandler : IRequestHandler<DeletarConvenioMedicoCommand, Unit>
    {
        private readonly IConvenioMedicoService _convenioService;

        public DeletarConvenioMedicoCommandHandler(IConvenioMedicoService convenioService)
        {
            _convenioService = convenioService;
        }

        public async Task<Unit> Handle(DeletarConvenioMedicoCommand request, CancellationToken cancellationToken)
        {
            await _convenioService.DeletarAsync(request.Id);
            
            return Unit.Value;
        }
    }
}