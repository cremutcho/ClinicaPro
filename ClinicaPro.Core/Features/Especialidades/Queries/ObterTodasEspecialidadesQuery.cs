// ClinicaPro.Core/Features/Especialidades/Queries/ObterTodasEspecialidadesQuery.cs
using MediatR;
using ClinicaPro.Core.Entities;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Especialidades.Queries
{
    public record ObterTodasEspecialidadesQuery : IRequest<IEnumerable<Especialidade>>;
}