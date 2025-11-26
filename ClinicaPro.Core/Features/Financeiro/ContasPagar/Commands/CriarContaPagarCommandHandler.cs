using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Commands
{
    public class CriarContaPagarCommandHandler : IRequestHandler<CriarContaPagarCommand, ContaPagar>
    {
        private readonly IContaPagarRepository _repository;

        public CriarContaPagarCommandHandler(IContaPagarRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContaPagar> Handle(CriarContaPagarCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Conta);
            return request.Conta;
        }
    }
}
