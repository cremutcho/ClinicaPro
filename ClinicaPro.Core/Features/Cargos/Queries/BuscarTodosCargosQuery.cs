using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Cargos.Queries
{
    // A Query implementa IRequest<T>, onde T é a lista de DTOs esperada.
    public record BuscarTodosCargosQuery : IRequest<IEnumerable<CargoDto>>;
}