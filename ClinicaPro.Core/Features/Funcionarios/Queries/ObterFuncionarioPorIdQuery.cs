using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries
{
    // Retorna Funcionario? (anulável) para lidar com ID não encontrado
    public record ObterFuncionarioPorIdQuery(int Id) : IRequest<Funcionario?>;
}