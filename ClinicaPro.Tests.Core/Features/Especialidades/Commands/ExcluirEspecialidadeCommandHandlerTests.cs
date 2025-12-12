using ClinicaPro.Core.Features.Especialidades.Commands;
using ClinicaPro.Core.Features.Especialidades.Handlers;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Core.Features.Especialidades.Commands
{
    public class ExcluirEspecialidadeCommandHandlerTests
    {
        private readonly Mock<IEspecialidadeRepository> _repositoryMock;
        private readonly ExcluirEspecialidadeCommandHandler _handler;

        public ExcluirEspecialidadeCommandHandlerTests()
        {
            _repositoryMock = new Mock<IEspecialidadeRepository>();
            _handler = new ExcluirEspecialidadeCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Excluir_Especialidade_Existente()
        {
            // Arrange
            _repositoryMock.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            var command = new ExcluirEspecialidadeCommand { Id = 1 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_False_Se_Especialidade_Nao_Existir()
        {
            // Arrange
            _repositoryMock.Setup(r => r.ExistsAsync(99)).ReturnsAsync(false);

            var command = new ExcluirEspecialidadeCommand { Id = 99 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
