using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public class UpdateConsultaCommandHandler : IRequestHandler<UpdateConsultaCommand, Unit>
    {
        private readonly IConsultaService _consultaService;

        public UpdateConsultaCommandHandler(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        public async Task<Unit> Handle(UpdateConsultaCommand request, CancellationToken cancellationToken)
        {
            // ⚠️ Ajuste: usar o método correto da interface
            var conflito = await _consultaService.VerificaConflitoHorario(
                request.Consulta.MedicoId,
                request.Consulta.DataHora
            );

            if (conflito)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta neste horário.");

            await _consultaService.AtualizarAsync(request.Consulta);
            return Unit.Value;
        }
    }
}
