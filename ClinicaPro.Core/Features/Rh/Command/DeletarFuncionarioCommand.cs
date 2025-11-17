using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ClinicaPro.Core.Features.RH.Commands
{
    // ----------------------------------------------------------------------
    // 1. COMMAND (Requisição de Deleção)
    // ----------------------------------------------------------------------
    // Usamos um record para um Command simples, que apenas precisa do Id.
    public record DeletarFuncionarioCommand(int Id) : IRequest;

    // ----------------------------------------------------------------------
    // 2. HANDLER (Lógica de Deleção Lógica)
    // ----------------------------------------------------------------------
    public class DeletarFuncionarioCommandHandler : IRequestHandler<DeletarFuncionarioCommand>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public DeletarFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task Handle(DeletarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            // 1. Busca a entidade
            var funcionario = await _funcionarioRepository.GetByIdAsync(request.Id);

            if (funcionario == null)
            {
                // Uma boa prática é lançar uma exceção de Application/NotFound para o Controller tratar
                throw new ApplicationException($"Funcionário com Id {request.Id} não encontrado para exclusão.");
            }

            // 2. Executa a Lógica de Exclusão (Desativação)
            // Em vez de remover do banco, apenas marcamos como inativo.
            funcionario.Ativo = false;

            // 3. Persiste a alteração no status
            await _funcionarioRepository.UpdateAsync(funcionario);

            // O Command IRequest não retorna nada (void/Task)
            return;
        }
    }
}