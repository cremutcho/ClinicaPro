using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Cargos.Queries
{
    public class BuscarCargoPorIdHandler : IRequestHandler<BuscarCargoPorIdQuery, CargoDto?>
    {
        private readonly ICargoRepository _cargoRepository;

        public BuscarCargoPorIdHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<CargoDto?> Handle(BuscarCargoPorIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Busca a entidade pelo Id
            var cargo = await _cargoRepository.GetByIdAsync(request.Id);

            if (cargo == null)
            {
                return null;
            }

            // 2. Mapeamento da Entidade para o DTO
            var cargoDto = new CargoDto
            {
                Id = cargo.Id,
                Nome = cargo.Nome,
                Descricao = cargo.Descricao
            };

            return cargoDto;
        }
    }
}