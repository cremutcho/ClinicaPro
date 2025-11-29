using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pagamentos.Commands
{
    public class CriarPagamentoCommandHandler : IRequestHandler<CriarPagamentoCommand, Pagamento>
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public CriarPagamentoCommandHandler(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<Pagamento> Handle(CriarPagamentoCommand request, CancellationToken cancellationToken)
        {
            // Se a data vier vazia, define automaticamente
            if (request.Pagamento.DataPagamento == default ||
                request.Pagamento.DataPagamento == DateTime.MinValue)
            {
                request.Pagamento.DataPagamento = DateTime.Now;
            }

            await _pagamentoRepository.AddAsync(request.Pagamento);

            return request.Pagamento;
        }
    }
}
