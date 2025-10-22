using Xunit;
using ClinicaPro.Core.Features.Pacientes.Commands;
using ClinicaPro.Core.Entities;
using System;
using FluentValidation.TestHelper; // Pacote do FluentValidation para testes

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class CriarPacienteCommandValidatorTests
    {
        // Certifique-se de que a classe CriarPacienteCommandValidator existe no Core
        private readonly CriarPacienteCommandValidator _validator; 

        public CriarPacienteCommandValidatorTests()
        {
            // Instancia o validador para os testes
            _validator = new CriarPacienteCommandValidator();
        }

        // --- TESTES DE NOME ---
        
        [Fact]
        public void ShouldHaveError_WhenNomeIsEmpty()
        {
            // Arrange
            var command = new CriarPacienteCommand
            {
                // Testando a validação de Nome vazio
                Paciente = new Paciente { Nome = "", CPF = "12345678900" } 
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            // Verifica se há erro na propriedade Nome
            result.ShouldHaveValidationErrorFor(c => c.Paciente.Nome)
                  .WithErrorMessage("O nome do paciente é obrigatório."); 
        }

        [Fact]
        public void ShouldNotHaveError_WhenNomeIsProvided()
        {
            // Arrange
            var command = new CriarPacienteCommand
            {
                // Testando a validação de Nome preenchido
                Paciente = new Paciente { Nome = "Nome Válido", CPF = "12345678900" }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            // Verifica que não há erro na propriedade Nome
            result.ShouldNotHaveValidationErrorFor(c => c.Paciente.Nome);
        }

        // --- TESTES DE CPF (NOVAS ATUALIZAÇÕES) ---

        [Fact]
        public void ShouldHaveError_WhenCpfIsEmpty()
        {
            // Arrange
            var command = new CriarPacienteCommand
            {
                // Nome Válido, mas CPF VAZIO
                Paciente = new Paciente { Nome = "Nome Válido", CPF = "" } 
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            // Verifica se há erro na propriedade CPF
            result.ShouldHaveValidationErrorFor(c => c.Paciente.CPF)
                  .WithErrorMessage("O CPF do paciente é obrigatório."); 
        }

        [Fact]
        public void ShouldNotHaveError_WhenCpfIsProvided()
        {
            // Arrange
            var command = new CriarPacienteCommand
            {
                // Nome e CPF Válidos
                Paciente = new Paciente { Nome = "Nome Válido", CPF = "123.456.789-00" }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            // Verifica que não há erro na propriedade CPF
            result.ShouldNotHaveValidationErrorFor(c => c.Paciente.CPF);
        }
    }
}