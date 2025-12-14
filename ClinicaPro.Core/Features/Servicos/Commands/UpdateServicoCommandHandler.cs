using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Servicos.Commands
{
    public class UpdateServicoCommandHandler : IRequestHandler<UpdateServicoCommand, bool>
    {
        private readonly IServicoRepository _repository;

        public UpdateServicoCommandHandler(IServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateServicoCommand request, CancellationToken cancellationToken)
        {
            var servico = await _repository.GetByIdAsync(request.Id);
            if (servico == null) return false;

            // 🔒 Validação de nome duplicado
            var todos = await _repository.GetAllAsync();
            if (todos.Any(s => s.Nome.ToLower() == request.Nome.ToLower() && s.Id != request.Id))
                throw new BusinessException("Já existe outro serviço cadastrado com este nome.");

            servico.Nome = request.Nome;
            await _repository.UpdateAsync(servico);
            return true;
        }
    }
}
