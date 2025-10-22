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
            // NOVO PADRÃO: A entidade Medico JÁ ESTÁ PRONTA dentro do Command.
            // O Validador garantiu que ela é válida antes de chegar aqui.
            var medico = request.Medico; 

            // Persiste a entidade que veio no Command.
            await _medicoRepository.AddAsync(medico); // Adicionei 'cancellationToken' por boas práticas

            // Retorna o ID gerado
            return medico.Id;
        }
    }
}