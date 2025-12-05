using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
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
