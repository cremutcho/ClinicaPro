using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Pacientes.Queries
{
    public class GetPacientesQuery : IRequest<List<Paciente>>
    {
    }
}
