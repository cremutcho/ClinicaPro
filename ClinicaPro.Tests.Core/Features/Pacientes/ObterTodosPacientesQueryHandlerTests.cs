using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Queries;
using System.Threading;

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class ObterTodosPacientesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarTodosPacientes_QuandoExistemDados()
        {
            // Arrange
            var mockRepo = new Mock<IPacienteRepository>();
            
            // 1. Cria dados de teste (usando o mínimo de propriedades)
            var pacientesDeTeste = new List<Paciente>
            {
                new Paciente { Id = 1, Nome = "João Silva", DataNascimento = new System.DateTime(1990, 1, 1) },
                new Paciente { Id = 2, Nome = "Maria Souza", DataNascimento = new System.DateTime(1985, 5, 15) }
            };

            // 2. Configura o mock para retornar a lista de teste
            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(pacientesDeTeste);

            var handler = new ObterTodosPacientesQueryHandler(mockRepo.Object);
            var query = new ObterTodosPacientesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            // 3. Verifica se a contagem corresponde aos dados de teste
            Assert.Equal(2, result.Count()); 
            
            // 4. Verifica se o método do repositório foi chamado
            mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once); 
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHaPacientes()
        {
            // Arrange
            var mockRepo = new Mock<IPacienteRepository>();
            
            // Configurar o mock para retornar uma lista vazia
            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(Enumerable.Empty<Paciente>());

            var handler = new ObterTodosPacientesQueryHandler(mockRepo.Object);
            var query = new ObterTodosPacientesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once); 
        }
    }
}