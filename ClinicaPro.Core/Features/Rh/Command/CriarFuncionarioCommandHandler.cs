using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.RH.Commands
{
    // O Handler processa o Command e executa a lógica de negócios
    public class CriarFuncionarioCommandHandler : IRequestHandler<CriarFuncionarioCommand, int>
    {
        // Injetamos a interface do Repositório
        private readonly IFuncionarioRepository _funcionarioRepository;

        public CriarFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<int> Handle(CriarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            // 1. Mapeamento do DTO (Command) para a Entidade de Domínio (Funcionario)
            // Usamos os campos da sua entidade: Nome, Sobrenome, CPF, etc.
            var novoFuncionario = new Funcionario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                CPF = request.CPF,
                Cargo = request.Cargo,
                DataContratacao = request.DataContratacao,
                Ativo = true // Assumindo que o funcionário é ativo ao ser contratado
            };

            // 2. Persistência dos dados através do Repositório
            await _funcionarioRepository.AddAsync(novoFuncionario);
            
            // 3. Retorna o ID (int) do novo funcionário
            return novoFuncionario.Id;
        }
    }
}