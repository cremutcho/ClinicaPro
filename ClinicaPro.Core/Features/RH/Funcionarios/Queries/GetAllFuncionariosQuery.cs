using MediatR;
using ClinicaPro.Core.Entities;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetAllFuncionarios
{
    public class GetAllFuncionariosQuery : IRequest<IEnumerable<Funcionario>>
    {
    }
}
