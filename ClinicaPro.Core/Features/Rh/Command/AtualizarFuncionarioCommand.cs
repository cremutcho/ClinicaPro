using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ClinicaPro.Core.Features.RH.Commands
{
    // ----------------------------------------------------------------------
    // 1. COMMAND (Requisição de Atualização)
    // ----------------------------------------------------------------------
    // O Command de atualização é similar ao de criação, mas inclui o Id.
    public class AtualizarFuncionarioCommand : IRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cargo { get; set; } = string.Empty;
        public decimal Salario { get; set; }
        public bool Ativo { get; set; }

        // Construtor auxiliar para mapear dados da Query (usado no GET Edit)
        public AtualizarFuncionarioCommand(int id, string nome, string sobrenome, string cpf, DateTime dataNascimento, string cargo, decimal salario, bool ativo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Cargo = cargo;
            Salario = salario;
            Ativo = ativo;
        }

        // Construtor padrão necessário para o Model Binding no POST
        public AtualizarFuncionarioCommand() { }
    }

    // ----------------------------------------------------------------------
    // 2. HANDLER (Lógica de Atualização)
    // ----------------------------------------------------------------------
    public class AtualizarFuncionarioCommandHandler : IRequestHandler<AtualizarFuncionarioCommand>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public AtualizarFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task Handle(AtualizarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            // 1. Busca a entidade existente
            var funcionario = await _funcionarioRepository.GetByIdAsync(request.Id);

            if (funcionario == null)
            {
                // Lança exceção para o Controller tratar como 404
                throw new ApplicationException($"Funcionário com Id {request.Id} não encontrado para atualização.");
            }

            // 2. Aplica as alterações na Entidade de Domínio
            funcionario.Nome = request.Nome;
            funcionario.Sobrenome = request.Sobrenome;
            funcionario.CPF = request.CPF;
            funcionario.Cargo = request.Cargo;
            funcionario.Ativo = request.Ativo;
            // 3. Persiste a alteração
            await _funcionarioRepository.UpdateAsync(funcionario);

            return;
        }
    }
}