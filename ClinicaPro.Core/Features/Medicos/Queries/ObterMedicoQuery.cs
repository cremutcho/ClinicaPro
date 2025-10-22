using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // A Query recebe o ID e retorna um objeto Medico (pode ser nullable)
    public record ObterMedicoQuery(int Id) : IRequest<Medico?>;
}