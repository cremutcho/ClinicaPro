using Xunit;
using System;
using ClinicaPro.Core.Features.Consultas.Commands;
using ClinicaPro.Core.Entities;
using FluentValidation.TestHelper; 

// SE PRECISO: Se o Enum StatusConsulta estiver em outro arquivo, 
// você precisa descomentar e ajustar o using abaixo
// using ClinicaPro.Core.Entities.Enums; 

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    // A DEFINIÇÃO DA CLASSE ESTAVA FALTANDO/INCOMPLETA NO TRECHO QUE VOCÊ ME MOSTROU
    public class CriarConsultaCommandValidatorTests 
    {
        private readonly CriarConsultaCommandValidator _validator;

        public CriarConsultaCommandValidatorTests()
        {
            _validator = new CriarConsultaCommandValidator();
        }

        // --- TESTES DE SUCESSO ---
        [Fact]
        public void ShouldNotHaveError_WhenAllFieldsAreValid()
        {
            var command = new CriarConsultaCommand
            {
                Consulta = new Consulta 
                { 
                    MedicoId = 1, 
                    PacienteId = 1, 
                    DataHora = DateTime.Now.AddDays(1),
                    Status = StatusConsulta.Agendada // Use o valor real do seu Enum!
                }
            };
            
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        // --- TESTES DE ERRO (MEDICO ID) ---
        [Fact]
        public void ShouldHaveError_WhenMedicoIdIsZero()
        {
            var command = new CriarConsultaCommand
            {
                Consulta = new Consulta 
                { 
                    MedicoId = 0, // ID Inválido
                    PacienteId = 1, 
                    DataHora = DateTime.Now.AddDays(1),
                    Status = StatusConsulta.Agendada 
                }
            };
            
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Consulta.MedicoId)
                  .WithErrorMessage("O ID do médico é obrigatório e deve ser válido."); 
        }

        // --- TESTES DE ERRO (PACIENTE ID) ---
        [Fact]
        public void ShouldHaveError_WhenPacienteIdIsZero()
        {
            var command = new CriarConsultaCommand
            {
                Consulta = new Consulta 
                { 
                    MedicoId = 1, 
                    PacienteId = 0, // ID Inválido
                    DataHora = DateTime.Now.AddDays(1),
                    Status = StatusConsulta.Agendada 
                }
            };
            
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Consulta.PacienteId)
                  .WithErrorMessage("O ID do paciente é obrigatório e deve ser válido."); 
        }

        // --- TESTES DE ERRO (DATA/HORA PASSADA) ---
        [Fact]
        public void ShouldHaveError_WhenDataHoraIsInThePast()
        {
            var command = new CriarConsultaCommand
            {
                Consulta = new Consulta 
                { 
                    MedicoId = 1, 
                    PacienteId = 1, 
                    DataHora = DateTime.Now.AddDays(-1), // Data no passado
                    Status = StatusConsulta.Agendada
                }
            };
            
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Consulta.DataHora)
                  .WithErrorMessage("A data e hora da consulta deve ser futura."); 
        }
    }
}