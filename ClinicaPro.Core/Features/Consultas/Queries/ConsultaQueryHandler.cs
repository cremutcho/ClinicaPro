using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public class ObterConsultaQueryHandler : IRequestHandler<ObterConsultaQuery, Consulta?>
    {
        private readonly IConsultaRepository _consultaRepository;

        public ObterConsultaQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Consulta?> Handle(ObterConsultaQuery request, CancellationToken cancellationToken)
        {
            // Delega a busca ao repositório (sem CancellationToken)
            return await _consultaRepository.GetByIdAsync(request.Id);
        }
    }
}