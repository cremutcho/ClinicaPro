using MediatR;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public record ConsultaExisteQuery(int Id) : IRequest<bool>;
}