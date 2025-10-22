using FluentValidation;
using ClinicaPro.Core.Features.Medicos.Commands;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    // O validador herda de AbstractValidator e opera sobre o CriarMedicoCommand
    public class CriarMedicoCommandValidator : AbstractValidator<CriarMedicoCommand>
    {
        public CriarMedicoCommandValidator()
        {
            // Regra 1: O Nome do médico não pode ser nulo ou vazio
            RuleFor(command => command.Medico.Nome)
                .NotEmpty()
                .WithMessage("O nome do médico é obrigatório.");

            // Regra 2: O CRM do médico não pode ser nulo ou vazio
            RuleFor(command => command.Medico.CRM)
                .NotEmpty()
                .WithMessage("O CRM do médico é obrigatório.");

            // Regra 3: O Id da Especialidade deve ser maior que zero (assume que EspecialidadeId é required)
            RuleFor(command => command.Medico.EspecialidadeId)
                .GreaterThan(0)
                .WithMessage("Uma especialidade válida deve ser selecionada.");
            
            // Regra 4: Limite de caracteres para o nome
            RuleFor(command => command.Medico.Nome)
                .MaximumLength(150)
                .WithMessage("O nome não pode exceder 150 caracteres.");
        }
    }
}