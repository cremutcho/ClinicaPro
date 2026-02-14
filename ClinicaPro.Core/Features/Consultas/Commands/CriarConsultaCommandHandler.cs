using System;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public class CriarConsultaCommandHandler 
        : IRequestHandler<CriarConsultaCommand, Unit>
    {
        private readonly IConsultaService _service;

        public CriarConsultaCommandHandler(IConsultaService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(
            CriarConsultaCommand request,
            CancellationToken cancellationToken)
        {
            var consulta = request.Consulta;

            var conflito = await _service.VerificaConflitoHorario(
                consulta.MedicoId,
                consulta.DataHora,
                null
            );

            if (conflito)
                throw new InvalidOperationException(
                    "Já existe uma consulta para este médico nesse horário."
                );

            await _service.CriarAsync(consulta);

            return Unit.Value;
        }
    }
}
