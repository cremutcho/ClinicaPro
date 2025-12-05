using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Servicos.Commands
{
    public class DeleteServicoCommandHandler : IRequestHandler<DeleteServicoCommand, bool>
    {
        private readonly IServicoRepository _servicoRepository;

        public DeleteServicoCommandHandler(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<bool> Handle(DeleteServicoCommand request, CancellationToken cancellationToken)
        {
            await _servicoRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
