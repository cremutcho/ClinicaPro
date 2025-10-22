using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Queries;
using System.Threading;

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class ObterTodosMedicosQueryHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarTodosMedicos_QuandoExistemDados()
        {
            // Arrange
            var mockRepo = new Mock<IMedicoRepository>();
            
            // 1. Cria dados de teste
            var medicosDeTeste = new List<Medico>
            {
                new Medico { Id = 1, Nome = "Dr. Silva", CRM = "123456", EspecialidadeId = 1 },
                new Medico { Id = 2, Nome = "Dra. Souza", CRM = "654321", EspecialidadeId = 2 }
            };

            // 2. Configura o mock para retornar a lista de teste
            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(medicosDeTeste);

            var handler = new ObterTodosMedicosQueryHandler(mockRepo.Object);
            var query = new ObterTodosMedicosQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            // 3. Verifica se a contagem corresponde aos dados de teste
            Assert.Equal(2, result.Count()); 
            
            // 4. Verifica se o método do repositório foi chamado
            mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once); 
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHaMedicos()
        {
            // Arrange
            var mockRepo = new Mock<IMedicoRepository>();
            
            // Configurar o mock para retornar uma lista vazia
            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(Enumerable.Empty<Medico>());

            var handler = new ObterTodosMedicosQueryHandler(mockRepo.Object);
            var query = new ObterTodosMedicosQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once); 
        }
    }
}