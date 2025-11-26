using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Commands
{
    public class CriarContaReceberCommandHandler : IRequestHandler<CriarContaReceberCommand, ContaReceber>
    {
        private readonly IContaReceberRepository _repository;

        public CriarContaReceberCommandHandler(IContaReceberRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContaReceber> Handle(CriarContaReceberCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Conta);
            return request.Conta;
        }
    }
}
