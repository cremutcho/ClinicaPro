using MediatR;
using ClinicaPro.Core.Entities;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Funcionarios.Queries
{
    // A Query não precisa de parâmetros para obter todos, retorna uma lista de Funcionario
    public record ObterTodosFuncionariosQuery : IRequest<IEnumerable<Funcionario>>;
}