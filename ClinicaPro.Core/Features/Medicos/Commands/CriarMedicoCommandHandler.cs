// ClinicaPro.Core/Features/Medicos/Commands/CriarMedicoCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    // O Handler implementa IRequestHandler<Command, Resultado>
    public class CriarMedicoCommandHandler : IRequestHandler<CriarMedicoCommand, int>
    {
        private readonly IMedicoRepository _medicoRepository;

        public CriarMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<int> Handle(CriarMedicoCommand request, CancellationToken cancellationToken)
        {
            // Mapeamento DTO (Command) para a Entidade
            var medico = new Medico
            {
                Nome = request.Nome,
                CRM = request.CRM,
                EspecialidadeId = request.EspecialidadeId,
                Email = request.Email,
                Telefone = request.Telefone
            };

            await _medicoRepository.AddAsync(medico);

            // Retorna o ID gerado
            return medico.Id;
        }
    }
}