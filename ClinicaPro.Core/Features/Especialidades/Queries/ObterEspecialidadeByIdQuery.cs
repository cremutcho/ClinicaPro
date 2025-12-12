using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Queries
{
    public class ObterTodasEspecialidadesQueryEspecialidadeByIdQuery : IRequest<Especialidade?>
    {
        public int Id { get; }

        public ObterTodasEspecialidadesQueryEspecialidadeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
