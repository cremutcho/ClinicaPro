using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Cargos.Commands
{
    public class AdicionarCargoHandler : IRequestHandler<AdicionarCargoCommand, int>
    {
        private readonly ICargoRepository _cargoRepository;

        // O Repositório é injetado via construtor
        public AdicionarCargoHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<int> Handle(AdicionarCargoCommand request, CancellationToken cancellationToken)
        {
            // 1. **Regra de Negócio (Exemplo):** Verifica se já existe um cargo com o mesmo nome
            var cargoExistente = await _cargoRepository.GetByNomeAsync(request.Nome);
            if (cargoExistente != null)
            {
                // Em CQRS, é comum lançar exceções para erros de negócio
                throw new ApplicationException($"Já existe um cargo cadastrado com o nome: {request.Nome}.");
            }
            
            // 2. Mapeamento de Command para Entidade
            var novoCargo = new Cargo
            {
                Nome = request.Nome,
                Descricao = request.Descricao
            };

            // 3. Persistência
            await _cargoRepository.AddAsync(novoCargo);

            // 4. Retorno do Resultado
            return novoCargo.Id;
        }
    }
}