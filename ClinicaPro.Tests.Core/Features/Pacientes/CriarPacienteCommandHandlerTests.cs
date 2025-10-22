using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Commands;
using System;
// using FluentValidation; // REMOVIDO: Não é necessário neste arquivo

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class CriarPacienteCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarAddAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IPacienteRepository>();
            
            // 1. Cria a entidade Paciente para ser passada
            var novoPaciente = new Paciente 
            { 
                Id = 10, // ID fictício
                Nome = "Ana Teste", 
                DataNascimento = new DateTime(1995, 8, 20), 
                CPF = "123.456.789-00",
                Telefone = "11998877665",
                Email = "ana.teste@exemplo.com",
                Endereco = "Rua Teste, 123" 
            };

            // 2. Cria o Command com a entidade Paciente
            var command = new CriarPacienteCommand { Paciente = novoPaciente };

            // Instancia o Handler
            var handler = new CriarPacienteCommandHandler(mockRepo.Object);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 3. Verifica se o método AddAsync foi chamado exatamente uma vez com o objeto Paciente correto.
            mockRepo.Verify(repo => repo.AddAsync(It.Is<Paciente>(
                p => p.Nome == novoPaciente.Nome && p.CPF == novoPaciente.CPF
            )), Times.Once);
        }
    }
}