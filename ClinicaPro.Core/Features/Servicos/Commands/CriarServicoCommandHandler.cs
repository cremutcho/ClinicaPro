using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Servicos.Commands
{
    public class CriarServicoCommandHandler : IRequestHandler<CriarServicoCommand, Servico>
    {
        private readonly IServicoRepository _repository;

        public CriarServicoCommandHandler(IServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Servico> Handle(CriarServicoCommand request, CancellationToken cancellationToken)
        {
            // 🔒 Validação de nome duplicado
            var todos = await _repository.GetAllAsync();
            if (todos.Any(s => s.Nome.ToLower() == request.Nome.ToLower()))
                throw new BusinessException("Já existe um serviço cadastrado com este nome.");

            var servico = new Servico
            {
                Nome = request.Nome,
                CodigoTuss = request.CodigoTuss,
                ValorPadrao = request.ValorPadrao
            };

            await _repository.AddAsync(servico);
            return servico;
        }
    }
}
