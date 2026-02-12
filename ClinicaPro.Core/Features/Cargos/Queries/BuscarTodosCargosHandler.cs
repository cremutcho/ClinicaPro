using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Cargos.Queries
{
    public class BuscarTodosCargosHandler : IRequestHandler<BuscarTodosCargosQuery, IEnumerable<CargoDto>>
    {
        private readonly ICargoRepository _cargoRepository;

        public BuscarTodosCargosHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<IEnumerable<CargoDto>> Handle(BuscarTodosCargosQuery request, CancellationToken cancellationToken)
        {
            // 1. Busca todos os cargos usando o repositório
            var cargos = await _cargoRepository.GetAllAsync();

            // 2. Mapeamento da Entidade (Cargo) para o DTO (CargoDto)
            var cargosDto = cargos.Select(c => new CargoDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao
            }).ToList();

            // 3. Retorna a lista de DTOs
            return cargosDto;
        }
    }
}