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
            // Verifica conflito de horário, ignorando a própria consulta
            var conflito = await _consultaService.VerificaConflitoHorarioAsync(
                request.Consulta.MedicoId,
                request.Consulta.DataHora,
                request.Consulta.Id);

            if (conflito)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta neste horário.");

            await _consultaService.AtualizarAsync(request.Consulta);
            return Unit.Value;
        }
    }
}
