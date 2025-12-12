using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Queries
{
    public class ObterEspecialidadePorIdQuery : IRequest<Especialidade?>
    {
        public int Id { get; }

        public ObterEspecialidadePorIdQuery(int id)
        {
            Id = id;
        }
    }
}
