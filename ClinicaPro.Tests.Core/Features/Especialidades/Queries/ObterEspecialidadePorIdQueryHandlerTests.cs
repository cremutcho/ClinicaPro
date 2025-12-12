using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Queries;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Core.Features.Especialidades.Queries
{
    public class ObterEspecialidadePorIdQueryHandlerTests
    {
        private readonly Mock<IEspecialidadeRepository> _repositoryMock;
        private readonly ObterEspecialidadePorIdQueryHandler _handler;

        public ObterEspecialidadePorIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<IEspecialidadeRepository>();
            _handler = new ObterEspecialidadePorIdQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarEspecialidade_QuandoEncontrada()
        {
            // Arrange
            var especialidade = new Especialidade
            {
                Id = 1,
                Nome = "Cardiologia"
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(especialidade);

            var query = new ObterEspecialidadePorIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Cardiologia", result.Nome);
        }

        [Fact]
        public async Task Handle_DeveRetornarNull_QuandoNaoEncontrada()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Especialidade?)null);

            var query = new ObterEspecialidadePorIdQuery(99);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
