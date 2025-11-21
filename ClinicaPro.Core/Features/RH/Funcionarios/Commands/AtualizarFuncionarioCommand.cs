using MediatR;

namespace ClinicaPro.Core.Features.RH.Funcionarios.Commands
{
    public class AtualizarFuncionarioCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int CargoId { get; set; }
        public bool Ativo { get; set; }
    }
}
