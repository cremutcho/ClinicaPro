using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public class ObterTodasConsultasQueryHandler : IRequestHandler<ObterTodasConsultasQuery, IEnumerable<Consulta>>
    {
        private readonly IConsultaRepository _consultaRepository; 

        public ObterTodasConsultasQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<IEnumerable<Consulta>> Handle(ObterTodasConsultasQuery request, CancellationToken cancellationToken)
        {
            var consultas = await _consultaRepository.GetAllAsync();

            // Retorna coleção vazia se o repositório retornar null
            return consultas ?? Enumerable.Empty<Consulta>();
        }
    }
}
