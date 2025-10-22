using FluentValidation;

namespace ClinicaPro.Core.Features.Pacientes.Commands
{
    // O validador DEVE ter o mesmo nome que o teste procura!
    public class CriarPacienteCommandValidator : AbstractValidator<CriarPacienteCommand>
    {
        public CriarPacienteCommandValidator()
        {
            // Regra 1: O Nome não pode ser nulo ou vazio
            RuleFor(command => command.Paciente.Nome)
                .NotEmpty()
                .WithMessage("O nome do paciente é obrigatório.");

            // Regra 2: O CPF deve ser preenchido
            RuleFor(command => command.Paciente.CPF)
                .NotEmpty()
                .WithMessage("O CPF do paciente é obrigatório.");

            // Regra 3 (Exemplo): O Nome deve ter um comprimento máximo
            RuleFor(command => command.Paciente.Nome)
                .MaximumLength(100)
                .WithMessage("O nome não pode exceder 100 caracteres.");
        }
    }
}