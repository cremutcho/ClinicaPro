using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq; // Necessário para o Enumerable.Empty
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
            // 1. Executa a busca no repositório.
            var consultas = await _consultaRepository.GetAllAsync();
            
            // 2. ✅ CORREÇÃO DE SEGURANÇA:
            // Garante que o retorno é uma coleção vazia se o repositório retornar null
            // (embora GetAllAsync() devesse retornar uma coleção vazia por padrão).
            return consultas ?? Enumerable.Empty<Consulta>();
        }
    }
}