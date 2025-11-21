using ClinicaPro.Core.Features.RH.Funcionarios.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.RH.Funcionarios.Handlers
{
    public class AlterarStatusFuncionarioCommandHandler 
        : IRequestHandler<AlterarStatusFuncionarioCommand, bool>
    {
        private readonly IFuncionarioRepository _repo;

        public AlterarStatusFuncionarioCommandHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(AlterarStatusFuncionarioCommand request, CancellationToken cancellationToken)
        {
            var funcionario = await _repo.GetByIdAsync(request.Id);

            if (funcionario == null)
                return false;

            funcionario.Ativo = request.NovoStatusAtivo;

            await _repo.UpdateAsync(funcionario);

            return true;
        }
    }
}
