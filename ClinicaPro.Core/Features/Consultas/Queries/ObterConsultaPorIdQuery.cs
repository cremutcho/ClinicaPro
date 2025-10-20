using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    public record ObterConsultaPorIdQuery(int Id) : IRequest<Consulta>;
}