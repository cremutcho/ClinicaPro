using MediatR;
using ClinicaPro.Core.Interfaces; // Necessário para IConsultaRepository
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    // O Handler implementa IRequestHandler para o comando e seu tipo de retorno (Unit)
    public class AtualizarConsultaCommandHandler : IRequestHandler<AtualizarConsultaCommand, Unit>
    {
        private readonly IConsultaRepository _consultaRepository;

        // Injeção de dependência do repositório
        public AtualizarConsultaCommandHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Unit> Handle(AtualizarConsultaCommand request, CancellationToken cancellationToken)
        {
            // 1. A lógica principal é chamar o método UpdateAsync do repositório
            await _consultaRepository.UpdateAsync(request.Consulta);
            
            // 2. Retorna Unit.Value para indicar que o comando foi executado com sucesso
            return Unit.Value;
        }
    }
}