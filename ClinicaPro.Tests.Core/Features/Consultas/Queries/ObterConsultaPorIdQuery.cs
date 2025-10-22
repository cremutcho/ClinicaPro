using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces; // Necessário para IConsultaRepository
using System.Threading;
using System.Threading.Tasks;

// ESTE NAMESPACE É CRÍTICO!
namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public class ObterConsultaPorIdQueryHandler : IRequestHandler<ObterConsultaPorIdQuery, Consulta?>
    {
        private readonly IConsultaRepository _consultaRepository;

        public ObterConsultaPorIdQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Consulta?> Handle(ObterConsultaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _consultaRepository.GetByIdAsync(request.Id);
        }
    }
}