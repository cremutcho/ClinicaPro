using Xunit;
using ClinicaPro.Core.Features.Medicos.Commands;
using ClinicaPro.Core.Entities;
using FluentValidation.TestHelper; 

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class CriarMedicoCommandValidatorTests
    {
        private readonly CriarMedicoCommandValidator _validator;

        public CriarMedicoCommandValidatorTests()
        {
            _validator = new CriarMedicoCommandValidator();
        }

        // --- TESTES DE NOME ---
        [Fact]
        public void ShouldHaveError_WhenNomeIsEmpty()
        {
            var command = new CriarMedicoCommand
            {
                Medico = new Medico { Nome = "", CRM = "CRM-12345", EspecialidadeId = 1 } 
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Medico.Nome)
                  .WithErrorMessage("O nome do médico é obrigatório."); 
        }
        
        // --- TESTES DE CRM ---
        [Fact]
        public void ShouldHaveError_WhenCrmIsEmpty()
        {
            var command = new CriarMedicoCommand
            {
                Medico = new Medico { Nome = "Dr. Teste", CRM = "", EspecialidadeId = 1 } 
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Medico.CRM)
                  .WithErrorMessage("O CRM do médico é obrigatório."); 
        }

        // --- TESTES DE ESPECIALIDADE ---
        [Fact]
        public void ShouldHaveError_WhenEspecialidadeIdIsZero()
        {
            var command = new CriarMedicoCommand
            {
                // EspecialidadeId = 0 (Valor inválido)
                Medico = new Medico { Nome = "Dr. Teste", CRM = "CRM-12345", EspecialidadeId = 0 } 
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Medico.EspecialidadeId)
                  .WithErrorMessage("Uma especialidade válida deve ser selecionada."); 
        }
        
        // --- TESTE DE SUCESSO (Deve passar em todos os campos) ---
        [Fact]
        public void ShouldNotHaveError_WhenAllFieldsAreValid()
        {
            var command = new CriarMedicoCommand
            {
                Medico = new Medico { Nome = "Dr. Teste", CRM = "CRM-12345", EspecialidadeId = 1 }
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}