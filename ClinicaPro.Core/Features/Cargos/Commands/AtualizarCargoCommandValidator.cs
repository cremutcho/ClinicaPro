using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Cargos.Commands
{
    public class AtualizarCargoHandler : IRequestHandler<AtualizarCargoCommand, Unit>
    {
        private readonly ICargoRepository _cargoRepository;

        public AtualizarCargoHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<Unit> Handle(AtualizarCargoCommand request, CancellationToken cancellationToken)
        {
            // 1. Busca o Cargo existente
            var cargo = await _cargoRepository.GetByIdAsync(request.Id);

            if (cargo == null)
            {
                // Lançar exceção se o registro não for encontrado
                throw new ApplicationException($"Cargo com Id {request.Id} não encontrado.");
            }

            // 2. Regra de Negócio: Verifica se o novo nome já está em uso por OUTRO cargo
            var cargoComMesmoNome = await _cargoRepository.GetByNomeAsync(request.Nome);
            if (cargoComMesmoNome != null && cargoComMesmoNome.Id != request.Id)
            {
                throw new ApplicationException($"Já existe outro cargo cadastrado com o nome: {request.Nome}.");
            }
            
            // 3. Aplica as mudanças
            cargo.Nome = request.Nome;
            cargo.Descricao = request.Descricao;

            // 4. Persistência
            await _cargoRepository.UpdateAsync(cargo);

            // Retorna Unit.Value para sinalizar que a operação foi concluída.
            return Unit.Value;
        }
    }
}