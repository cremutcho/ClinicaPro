using ClinicaPro.Core.Interfaces;
using MediatR;
using ConvenioEntity = ClinicaPro.Core.Entities.ConvenioMedico;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    public class GetConvenioMedicoQueryHandler 
        : IRequestHandler<GetConvenioMedicoQuery, List<ConvenioEntity>>
    {
        private readonly IConvenioMedicoService _convenioService;

        public GetConvenioMedicoQueryHandler(IConvenioMedicoService convenioService)
        {
            _convenioService = convenioService;
        }

        public async Task<List<ConvenioEntity>> Handle(
            GetConvenioMedicoQuery request,
            CancellationToken cancellationToken)
        {
            var convenios = await _convenioService.ListarAsync();
            return convenios.ToList();
        }
    }
}
