using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public class CriarConsultaCommandHandler : IRequestHandler<CriarConsultaCommand, Unit>
    {
        private readonly IConsultaService _consultaService;

        public CriarConsultaCommandHandler(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        public async Task<Unit> Handle(CriarConsultaCommand request, CancellationToken cancellationToken)
        {
            await _consultaService.CriarAsync(request.Consulta);
            return Unit.Value;
        }
    }
}
