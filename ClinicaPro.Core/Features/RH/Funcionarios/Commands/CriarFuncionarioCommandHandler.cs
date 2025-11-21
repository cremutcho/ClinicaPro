using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.RH.Funcionarios.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.RH.Funcionarios.Handlers
{
    public class CriarFuncionarioCommandHandler 
        : IRequestHandler<CriarFuncionarioCommand, int>
    {
        private readonly IFuncionarioRepository _repo;

        public CriarFuncionarioCommandHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CriarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            var funcionario = new Funcionario
            {
                Nome = request.Nome,
                CPF = request.CPF,
                Email = request.Email,
                Telefone = request.Telefone,
                DataNascimento = request.DataNascimento,
                DataAdmissao = request.DataAdmissao,
                CargoId = request.CargoId,
                Ativo = true
            };

            await _repo.AddAsync(funcionario);

            return funcionario.Id;
        }
    }
}
