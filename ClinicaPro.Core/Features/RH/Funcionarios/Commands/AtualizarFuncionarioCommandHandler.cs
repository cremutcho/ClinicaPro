using ClinicaPro.Core.Features.RH.Funcionarios.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.RH.Funcionarios.Handlers
{
    public class AtualizarFuncionarioCommandHandler 
        : IRequestHandler<AtualizarFuncionarioCommand, bool>
    {
        private readonly IFuncionarioRepository _repo;

        public AtualizarFuncionarioCommandHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(AtualizarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            var funcionario = await _repo.GetByIdAsync(request.Id);

            if (funcionario == null)
                return false;

            funcionario.Nome = request.Nome;
            funcionario.CPF = request.CPF;
            funcionario.Email = request.Email;
            funcionario.Telefone = request.Telefone;
            funcionario.DataNascimento = request.DataNascimento;
            funcionario.DataAdmissao = request.DataAdmissao;
            funcionario.CargoId = request.CargoId;
            funcionario.Ativo = request.Ativo;

            await _repo.UpdateAsync(funcionario);

            return true;
        }
    }
}
