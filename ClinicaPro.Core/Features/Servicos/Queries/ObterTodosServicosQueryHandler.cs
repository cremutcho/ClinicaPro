using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Servicos.Queries
{
    public class ObterTodosServicosQueryHandler 
        : IRequestHandler<ObterTodosServicosQuery, IEnumerable<Servico>>
    {
        private readonly IServicoRepository _servicoRepository;

        public ObterTodosServicosQueryHandler(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<IEnumerable<Servico>> Handle(
            ObterTodosServicosQuery request, 
            CancellationToken cancellationToken)
        {
            return await _servicoRepository.GetAllAsync();
        }
    }
}
