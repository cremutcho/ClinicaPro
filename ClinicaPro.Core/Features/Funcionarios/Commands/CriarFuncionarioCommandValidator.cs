using FluentValidation;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    public class CriarFuncionarioCommandValidator : AbstractValidator<CriarFuncionarioCommand>
    {
        private readonly IFuncionarioRepository _repository;

        // Injetamos o Repositório no construtor
        public CriarFuncionarioCommandValidator(IFuncionarioRepository repository)
        {
            _repository = repository;
            
            // Regra 1: Nome e Sobrenome
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .MaximumLength(100).WithMessage("O Nome não pode exceder 100 caracteres.");
            
            RuleFor(c => c.Sobrenome)
                .NotEmpty().WithMessage("O Sobrenome é obrigatório.");
            
            // Regra 2: CPF (Formato e Unicidade)
            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Length(11).WithMessage("O CPF deve ter 11 dígitos (apenas números).")
                // Chamada do método de unicidade
                .MustAsync(BeUniqueCpf).WithMessage("O CPF informado já está registrado em outro funcionário.");
            
            // Regra 3: Data de Contratação
            RuleFor(c => c.DataContratacao)
                .NotEmpty().WithMessage("A Data de Contratação é obrigatória.")
                .LessThanOrEqualTo(System.DateTime.Today).WithMessage("A Data de Contratação não pode ser futura.");
        }

        // Método de validação assíncrona que chama o Repositório
        private async Task<bool> BeUniqueCpf(string cpf, CancellationToken cancellationToken)
        {
            // Se o CPF existir, retorna 'true' (o MustAsync valida o oposto: MustAsync = true é válido)
            var existe = await _repository.ExisteCpfAsync(cpf);
            return !existe; // Retorna TRUE (VÁLIDO) se o CPF NÃO EXISTIR
        }
    }
}