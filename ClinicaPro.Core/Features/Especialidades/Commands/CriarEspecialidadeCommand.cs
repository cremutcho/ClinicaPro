using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Commands
{
    public class CriarEspecialidadeCommand : IRequest<Especialidade>
    {
        public string Nome { get; set; } = null!;
    }
}
