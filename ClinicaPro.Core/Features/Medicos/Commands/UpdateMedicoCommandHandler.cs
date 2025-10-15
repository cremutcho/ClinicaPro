// ClinicaPro.Core/Features/Medicos/Commands/UpdateMedicoCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic; // Para KeyNotFoundException

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public class UpdateMedicoCommandHandler : IRequestHandler<UpdateMedicoCommand, Unit>
    {
        private readonly IMedicoRepository _medicoRepository;

        public UpdateMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Unit> Handle(UpdateMedicoCommand request, CancellationToken cancellationToken)
        {
            var medico = await _medicoRepository.GetByIdAsync(request.Id);

            if (medico == null)
            {
                throw new KeyNotFoundException($"Médico com ID {request.Id} não encontrado.");
            }

            medico.Nome = request.Nome;
            medico.CRM = request.CRM;
            medico.EspecialidadeId = request.EspecialidadeId;
            medico.Email = request.Email;
            medico.Telefone = request.Telefone;

            await _medicoRepository.UpdateAsync(medico);

            return Unit.Value;
        }
    }
}