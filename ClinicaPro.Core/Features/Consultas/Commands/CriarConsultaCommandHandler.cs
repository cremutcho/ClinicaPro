using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic; // Para KeyNotFoundException

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public class CriarConsultaCommandHandler : IRequestHandler<CriarConsultaCommand, Unit>
    {
        private readonly IConsultaRepository _consultaRepository;

        public CriarConsultaCommandHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Unit> Handle(CriarConsultaCommand request, CancellationToken cancellationToken)
        {
            // Lógica de Negócio: Verifica conflito de horário antes de agendar (Crucial!)
            var conflito = await _consultaRepository.VerificaConflitoHorario(
                request.Consulta.MedicoId, 
                request.Consulta.DataHora);

            if (conflito)
            {
                // Alternativa: Lançar uma exceção de validação específica (ex: ConflitoHorarioException)
                throw new InvalidOperationException("Conflito de horário: O médico já tem uma consulta agendada para este horário.");
            }

            // Se não houver conflito, salva
            await _consultaRepository.AddAsync(request.Consulta);
            
            return Unit.Value;
        }
    }
}