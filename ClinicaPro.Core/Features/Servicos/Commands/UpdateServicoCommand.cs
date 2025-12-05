using MediatR;

namespace ClinicaPro.Core.Features.Servicos.Commands
{
    public class UpdateServicoCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
