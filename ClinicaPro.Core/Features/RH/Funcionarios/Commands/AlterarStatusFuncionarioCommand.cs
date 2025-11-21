using MediatR;

namespace ClinicaPro.Core.Features.RH.Funcionarios.Commands
{
    public class AlterarStatusFuncionarioCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public bool NovoStatusAtivo { get; set; }
    }
}
