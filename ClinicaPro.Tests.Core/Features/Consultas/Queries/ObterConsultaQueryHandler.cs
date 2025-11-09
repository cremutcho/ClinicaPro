using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    // Handler responsável por tratar a Query de "obter consulta"
    public class ObterConsultaQueryHandler : IRequestHandler<ObterConsultaQuery, Consulta?>
    {
        private readonly IConsultaRepository _consultaRepository;

        // Injeção de dependência do repositório
        public ObterConsultaQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        // Método Handle — executado quando a Query é chamada via MediatR
        public async Task<Consulta?> Handle(ObterConsultaQuery request, CancellationToken cancellationToken)
        {
            // Busca a consulta pelo ID usando o repositório
            var consulta = await _consultaRepository.GetByIdAsync(request.Id);

            // Retorna a consulta (ou null se não encontrada)
            return consulta;
        }
    }
}
