using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Servicos.Queries;
using System.Linq;

namespace ClinicaPro.Tests.Core.Features.Servicos.Queries
{
    public class ObterTodosServicosQueryHandlerTests
    {
        private readonly Mock<IServicoRepository> _repositoryMock;
        private readonly ObterTodosServicosQueryHandler _handler;

        public ObterTodosServicosQueryHandlerTests()
        {
            _repositoryMock = new Mock<IServicoRepository>();
            _handler = new ObterTodosServicosQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarTodosServicos()
        {
            // Arrange
            var servicos = new List<Servico>
            {
                new Servico { Id = 1, Nome = "Consulta Médica" },
                new Servico { Id = 2, Nome = "Exame Laboratorial" }
            };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(servicos);

            var query = new ObterTodosServicosQuery();

            // Act
            var result = (await _handler.Handle(query, CancellationToken.None)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Consulta Médica", result[0].Nome);
            Assert.Equal("Exame Laboratorial", result[1].Nome);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_SeNaoExistiremServicos()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Servico>());
            var query = new ObterTodosServicosQuery();

            // Act
            var result = (await _handler.Handle(query, CancellationToken.None)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
