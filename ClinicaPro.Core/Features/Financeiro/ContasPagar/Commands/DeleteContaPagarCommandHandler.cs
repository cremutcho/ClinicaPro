using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Commands
{
    public class DeleteContaPagarCommandHandler : IRequestHandler<DeleteContaPagarCommand, bool>
    {
        private readonly IContaPagarRepository _repo;

        public DeleteContaPagarCommandHandler(IContaPagarRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteContaPagarCommand request, CancellationToken cancellationToken)
        {
            var conta = await _repo.GetByIdAsync(request.Id);
            if (conta == null)
                return false;

            await _repo.DeleteAsync(request.Id);  // ✅ Ajuste correto
            return true;
        }
    }
}
