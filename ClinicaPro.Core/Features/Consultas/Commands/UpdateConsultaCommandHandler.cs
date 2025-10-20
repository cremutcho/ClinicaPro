using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public class UpdateConsultaCommandHandler : IRequestHandler<UpdateConsultaCommand, Unit>
    {
        private readonly IConsultaRepository _consultaRepository;

        public UpdateConsultaCommandHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Unit> Handle(UpdateConsultaCommand request, CancellationToken cancellationToken)
        {
            // O Command já assume que a entidade está "attachada" ou pronta para ser atualizada
            await _consultaRepository.UpdateAsync(request.Consulta);
            
            return Unit.Value;
        }
    }
}