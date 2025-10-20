// ClinicaPro.Core/Features/Pacientes/Queries/ObterTodosPacientesQuery.cs
using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Pacientes.Queries // PLURAL: Pacientes
{
    // A Query retorna uma lista de objetos Paciente
    public record ObterTodosPacientesQuery : IRequest<IEnumerable<Paciente>>;
}