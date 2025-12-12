using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Commands
{
    public class AtualizarEspecialidadeCommand : IRequest<Especialidade?>
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
    }
}
