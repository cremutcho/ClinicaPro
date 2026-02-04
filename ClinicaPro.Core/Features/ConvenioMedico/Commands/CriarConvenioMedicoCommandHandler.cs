using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.ConvenioMedico.Commands;
using MediatR;
using ConvenioEntity = ClinicaPro.Core.Entities.ConvenioMedico;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public class CriarConvenioMedicoCommandHandler
        : IRequestHandler<CriarConvenioMedicoCommand, ConvenioEntity>
    {
        private readonly IConvenioMedicoService _convenioService;

        public CriarConvenioMedicoCommandHandler(IConvenioMedicoService convenioService)
        {
            _convenioService = convenioService;
        }

        public async Task<ConvenioEntity> Handle(
            CriarConvenioMedicoCommand request,
            CancellationToken cancellationToken)
        {
            return await _convenioService.CriarAsync(request.ConvenioMedico);
        }
    }
}
