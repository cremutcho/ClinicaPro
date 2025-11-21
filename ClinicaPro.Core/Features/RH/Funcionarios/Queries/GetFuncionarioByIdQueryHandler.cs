using MediatR;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionarioById
{
    public class GetFuncionarioByIdQueryHandler 
        : IRequestHandler<GetFuncionarioByIdQuery, Funcionario?>
    {
        private readonly IFuncionarioRepository _repo;

        public GetFuncionarioByIdQueryHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<Funcionario?> Handle(
            GetFuncionarioByIdQuery request, 
            CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
