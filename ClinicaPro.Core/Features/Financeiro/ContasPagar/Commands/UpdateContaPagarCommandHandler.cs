using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Commands
{
    public class UpdateContaPagarCommandHandler : IRequestHandler<UpdateContaPagarCommand, bool>
    {
        private readonly IContaPagarRepository _repo;

        public UpdateContaPagarCommandHandler(IContaPagarRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateContaPagarCommand request, CancellationToken cancellationToken)
        {
            var conta = await _repo.GetByIdAsync(request.Id);
            if (conta == null)
                return false;

            conta.Descricao = request.Descricao;
            conta.Valor = request.Valor;
            conta.DataEmissao = request.DataEmissao;
            conta.DataVencimento = request.DataVencimento;
            conta.DataPagamento = request.DataPagamento;
            conta.Status = request.Status;
            conta.Categoria = request.Categoria;

            await _repo.UpdateAsync(conta);
            return true;
        }
    }
}
