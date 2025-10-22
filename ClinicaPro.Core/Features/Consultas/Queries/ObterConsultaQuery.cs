using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    // A Query recebe o ID e retorna um objeto Consulta (pode ser nullable)
    public record ObterConsultaQuery(int Id) : IRequest<Consulta?>;
}