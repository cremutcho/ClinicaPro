using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Queries;

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class ObterTodasConsultasQueryHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHaConsultas()
        {
            // Arrange
            // 1. Mock do IConsultaRepository
            var mockRepo = new Mock<IConsultaRepository>();
            
            // 2. Configurar o mock para retornar uma lista vazia quando GetAllAsync for chamado
            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(Enumerable.Empty<Consulta>());

            // 3. Criar o Handler, injetando o mock
            var handler = new ObterTodasConsultasQueryHandler(mockRepo.Object);
            var query = new ObterTodasConsultasQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            // 4. Verificar se o resultado não é nulo e está vazio
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_DeveRetornarTodasConsultas_QuandoExistemDados()
        {
            // Arrange
            var mockRepo = new Mock<IConsultaRepository>();
            
            // Cria dados de teste
            var consultasDeTeste = new List<Consulta>
            {
                new Consulta { Id = 1, DataHora = DateTime.Now.AddDays(-1), PacienteId = 1, MedicoId = 1 },
                new Consulta { Id = 2, DataHora = DateTime.Now, PacienteId = 2, MedicoId = 1 }
            };

            // Configurar o mock para retornar a lista de teste
            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(consultasDeTeste);

            var handler = new ObterTodasConsultasQueryHandler(mockRepo.Object);
            var query = new ObterTodasConsultasQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            // Verifica se a contagem corresponde aos dados de teste
            Assert.Equal(2, result.Count()); 
            
            // Verifica se o método do repositório foi chamado exatamente uma vez
            mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once); 
        }
    }
}