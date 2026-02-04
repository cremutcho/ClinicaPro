using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    public class GetAllConvenioMedicoQueryHandler 
        : IRequestHandler<GetAllConvenioMedicoQuery, List<ClinicaPro.Core.Entities.ConvenioMedico>>
    {
        private readonly IConvenioMedicoRepository _repository;

        public GetAllConvenioMedicoQueryHandler(IConvenioMedicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClinicaPro.Core.Entities.ConvenioMedico>> Handle(
            GetAllConvenioMedicoQuery request, 
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
