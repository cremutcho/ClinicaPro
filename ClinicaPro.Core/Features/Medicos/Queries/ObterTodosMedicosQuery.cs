// ClinicaPro.Core/Features/Medicos/Queries/ObterTodosMedicosQuery.cs
using MediatR;
using System.Collections.Generic;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // A Query, que é o objeto de requisição. 
    // Ela implementa IRequest<TResultado>
    public record ObterTodosMedicosQuery : IRequest<IEnumerable<Medico>>;
}