using FluentValidation;

namespace ClinicaPro.Core.Features.Cargos.Commands
{
    // O validador herda de AbstractValidator<T>, onde T é o Command que ele valida.
    public class AdicionarCargoCommandValidator : AbstractValidator<AdicionarCargoCommand>
    {
        public AdicionarCargoCommandValidator()
        {
            // Regra 1: O campo Nome é obrigatório e deve ter entre 3 e 100 caracteres.
            RuleFor(command => command.Nome)
                .NotEmpty().WithMessage("O nome do cargo é obrigatório.")
                .Length(3, 100).WithMessage("O nome do cargo deve ter entre 3 e 100 caracteres.");

            // Regra 2: O campo Descricao é obrigatório e deve ter entre 10 e 500 caracteres.
            RuleFor(command => command.Descricao)
                .NotEmpty().WithMessage("A descrição do cargo é obrigatória.")
                .Length(10, 500).WithMessage("A descrição deve ter entre 10 e 500 caracteres.");
        }
    }
}