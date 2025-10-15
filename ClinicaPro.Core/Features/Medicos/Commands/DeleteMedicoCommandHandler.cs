// ClinicaPro.Core/Features/Medicos/Commands/DeleteMedicoCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic; // Para KeyNotFoundException

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public class DeleteMedicoCommandHandler : IRequestHandler<DeleteMedicoCommand, Unit>
    {
        private readonly IMedicoRepository _medicoRepository;

        public DeleteMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Unit> Handle(DeleteMedicoCommand request, CancellationToken cancellationToken)
        {
            // O repositório já tem um método DeleteAsync que busca pelo ID e remove.
            // Se o repositório não tem lógica de verificação de existência, 
            // a camada de infraestrutura deve lidar com a exceção se o ID não for encontrado.
            
            // Para ser robusto, vamos buscar primeiro (opcional, mas seguro):
            var medico = await _medicoRepository.GetByIdAsync(request.Id);

            if (medico == null)
            {
                // Se o médico já foi excluído, podemos apenas retornar sucesso (Unit.Value)
                // ou lançar uma exceção
                return Unit.Value; 
            }

            await _medicoRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}