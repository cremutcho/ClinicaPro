using FluentValidation;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.ConvenioMedico.Commands
{
    public class CriarConvenioMedicoCommandValidator : AbstractValidator<CriarConvenioMedicoCommand>
    {
        public CriarConvenioMedicoCommandValidator()
        {
            RuleFor(c => c.ConvenioMedico)
                .NotNull().WithMessage("O convênio não pode ser nulo.");

            RuleFor(c => c.ConvenioMedico.Nome)
                .NotEmpty().WithMessage("O nome do convênio é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do convênio não pode ter mais de 100 caracteres.");

            // Opcional: Se quiser garantir que Ativo seja definido
            RuleFor(c => c.ConvenioMedico.Ativo)
                .NotNull().WithMessage("O status do convênio deve ser definido.");

            // Opcional: DataCadastro será definida no handler, mas pode validar se quiser
            RuleFor(c => c.ConvenioMedico.DataCadastro)
                .LessThanOrEqualTo(System.DateTime.UtcNow)
                .WithMessage("A data de cadastro não pode ser futura.");
        }
    }
}
