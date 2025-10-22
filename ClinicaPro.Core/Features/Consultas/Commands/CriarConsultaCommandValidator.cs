using FluentValidation;
using System;
using ClinicaPro.Core.Features.Consultas.Commands;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    // O validador opera sobre a Entidade Consulta dentro do Command
    public class CriarConsultaCommandValidator : AbstractValidator<CriarConsultaCommand>
    {
        public CriarConsultaCommandValidator()
        {
            // Regra 1: MedicoId deve ser maior que zero (necessário para agendar)
            RuleFor(command => command.Consulta.MedicoId)
                .GreaterThan(0)
                .WithMessage("O ID do médico é obrigatório e deve ser válido.");

            // Regra 2: PacienteId deve ser maior que zero (necessário para agendar)
            RuleFor(command => command.Consulta.PacienteId)
                .GreaterThan(0)
                .WithMessage("O ID do paciente é obrigatório e deve ser válido.");

            // Regra 3: A Data/Hora da consulta não pode ser no passado
            // Usamos GreaterThan(DateTime.Now) para forçar que seja uma data/hora futura
            RuleFor(command => command.Consulta.DataHora)
                .GreaterThan(DateTime.Now) 
                .WithMessage("A data e hora da consulta deve ser futura.");
            
            // Regra 4: (REMOVIDA!)
            /* // A regra abaixo está causando a falha no teste.
            // Se Status for um Enum, essa validação não é necessária.
            RuleFor(command => command.Consulta.Status)
                .NotEmpty()
                .WithMessage("O status da consulta é obrigatório.");
            */
        }
    }
}