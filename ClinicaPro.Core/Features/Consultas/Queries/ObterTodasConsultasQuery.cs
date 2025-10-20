using ClinicaPro.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Consultas.Queries
{
    // Retorna uma lista de consultas (com Medico e Paciente incluídos, se necessário)
    public record ObterTodasConsultasQuery : IRequest<IEnumerable<Consulta>>;
}