// ClinicaPro.Core/Features/Pacientes/Queries/ObterPacientePorIdQuery.cs
using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Pacientes.Queries
{
    // A Query precisa de um ID e retorna um único objeto Paciente (ou null)
    public record ObterPacientePorIdQuery : IRequest<Paciente?>
    {
        public int Id { get; init; }
    }
}