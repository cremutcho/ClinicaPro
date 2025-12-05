using MediatR;

namespace ClinicaPro.Core.Features.Servicos.Commands
{
    public class DeleteServicoCommand : IRequest<bool>
    {
        public int Id { get; set; }
        
    }
}
