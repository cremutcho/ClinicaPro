using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public class ConsultaExisteQueryHandler : IRequestHandler<ConsultaExisteQuery, bool>
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaExisteQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<bool> Handle(ConsultaExisteQuery request, CancellationToken cancellationToken)
        {
            // Usando o método existente do IRepository
            return await _consultaRepository.ExistsAsync(request.Id);
        }
    }
}