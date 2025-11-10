using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    // O Command recebe apenas o Id e retorna Unit (apenas sucesso/falha)
    public record DeletarFuncionarioCommand(int Id) : IRequest<Unit>;
}