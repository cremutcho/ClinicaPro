using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Servicos.Queries
{
    public class ObterServicoPorIdQueryHandler 
        : IRequestHandler<ObterServicoPorIdQuery, Servico?>
    {
        private readonly IServicoRepository _servicoRepository;

        public ObterServicoPorIdQueryHandler(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<Servico?> Handle(
            ObterServicoPorIdQuery request, 
            CancellationToken cancellationToken)
        {
            return await _servicoRepository.GetByIdAsync(request.Id);
        }
    }
}
