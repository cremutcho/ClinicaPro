using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Commands
{
    public class DeleteContaReceberCommandHandler : IRequestHandler<DeleteContaReceberCommand, bool>
    {
        private readonly IContaReceberRepository _repository;

        public DeleteContaReceberCommandHandler(IContaReceberRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteContaReceberCommand request, CancellationToken cancellationToken)
        {
            var conta = await _repository.GetByIdAsync(request.Id); // Usa só o Id
            if (conta == null)
                return false;

            await _repository.DeleteAsync(conta.Id);
            return true;
        }
    }
}
