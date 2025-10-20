using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public class DeleteConsultaCommandHandler : IRequestHandler<DeleteConsultaCommand, Unit>
    {
        private readonly IConsultaRepository _consultaRepository;

        public DeleteConsultaCommandHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Unit> Handle(DeleteConsultaCommand request, CancellationToken cancellationToken)
        {
            await _consultaRepository.DeleteAsync(request.Id);
            
            return Unit.Value;
        }
    }
}