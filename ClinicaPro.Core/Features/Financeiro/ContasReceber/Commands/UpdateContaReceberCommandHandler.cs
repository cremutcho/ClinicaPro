using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Commands
{
    public class UpdateContaReceberCommandHandler : IRequestHandler<UpdateContaReceberCommand, ContaReceber?>
    {
        private readonly IContaReceberRepository _repository;

        public UpdateContaReceberCommandHandler(IContaReceberRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContaReceber?> Handle(UpdateContaReceberCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Conta.Id);
            if (existing == null)
                return null; // Mantemos o retorno nulo seguro

            // Atualiza os campos
            existing.Descricao = request.Conta.Descricao;
            existing.Valor = request.Conta.Valor;
            existing.DataEmissao = request.Conta.DataEmissao;
            existing.DataVencimento = request.Conta.DataVencimento;
            existing.DataRecebimento = request.Conta.DataRecebimento;
            existing.Cliente = request.Conta.Cliente;

            await _repository.UpdateAsync(existing);
            return existing;
        }
    }
}
