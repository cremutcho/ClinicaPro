using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Pagamentos.Commands
{
    public class CriarPagamentoCommand : IRequest<Pagamento>
    {
        public Pagamento Pagamento { get; set; }

        public CriarPagamentoCommand(Pagamento pagamento)
        {
            Pagamento = pagamento;
        }
    }
}
