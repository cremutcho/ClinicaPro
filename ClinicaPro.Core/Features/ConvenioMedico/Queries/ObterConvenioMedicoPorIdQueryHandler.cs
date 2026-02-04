using ClinicaPro.Core.Interfaces;
using MediatR;
using ConvenioEntity = ClinicaPro.Core.Entities.ConvenioMedico;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    public class GetConvenioMedicoByIdQueryHandler : IRequestHandler<GetConvenioMedicoByIdQuery, ConvenioEntity?>
    {
        private readonly IConvenioMedicoService _convenioService;

        public GetConvenioMedicoByIdQueryHandler(IConvenioMedicoService convenioService)
        {
            _convenioService = convenioService;
        }

        public async Task<ConvenioEntity?> Handle(GetConvenioMedicoByIdQuery request, CancellationToken cancellationToken)
        {
            // Busca o convênio pelo Id usando o service
            var convenio = await _convenioService.BuscarPorIdAsync(request.Id);

            return convenio; // Pode retornar null se não encontrado
        }
    }
}
