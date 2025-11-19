using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public class ObterConsultaPorIdQueryHandler : IRequestHandler<ObterConsultaPorIdQuery, Consulta>
    {
        private readonly IConsultaRepository _consultaRepository;

        public ObterConsultaPorIdQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Consulta> Handle(ObterConsultaPorIdQuery request, CancellationToken cancellationToken)
        {
            var consulta = await _consultaRepository.GetByIdAsync(request.Id);

            if (consulta == null)
                throw new KeyNotFoundException($"Consulta com Id {request.Id} não encontrada.");

            return consulta;
        }
    }
}
