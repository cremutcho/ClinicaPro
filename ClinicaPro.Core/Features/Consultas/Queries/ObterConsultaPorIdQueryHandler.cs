// ----------------------------------------------------------------------
// Conteúdo CORRETO para o arquivo: ObterConsultaPorIdQueryHandler.cs
// ----------------------------------------------------------------------

using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces; // Para IConsultaRepository
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    // Apenas a classe Handler deve estar aqui.
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
            return consulta; 
        }
    }
}