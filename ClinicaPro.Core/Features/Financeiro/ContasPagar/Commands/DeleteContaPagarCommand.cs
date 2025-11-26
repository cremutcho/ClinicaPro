using MediatR;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Commands
{
    public class DeleteContaPagarCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
