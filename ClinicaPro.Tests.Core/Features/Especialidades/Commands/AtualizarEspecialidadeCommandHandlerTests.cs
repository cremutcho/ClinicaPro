using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Commands;
using ClinicaPro.Core.Features.Especialidades.Handlers;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Core.Features.Especialidades.Commands
{
    public class AtualizarEspecialidadeCommandHandlerTests
    {
        private readonly Mock<IEspecialidadeRepository> _repositoryMock;
        private readonly AtualizarEspecialidadeCommandHandler _handler;

        public AtualizarEspecialidadeCommandHandlerTests()
        {
            _repositoryMock = new Mock<IEspecialidadeRepository>();
            _handler = new AtualizarEspecialidadeCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Atualizar_Especialidade_Existente()
        {
            // Arrange
            var especialidade = new Especialidade
            {
                Id = 1,
                Nome = "Cardiologia"
            };

            var command = new AtualizarEspecialidadeCommand
            {
                Id = 1,
                Nome = "Neurologia"
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(especialidade);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Especialidade>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Neurologia", result.Nome);

            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Especialidade>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Se_Especialidade_Nao_Existir()
        {
            // Arrange
            var command = new AtualizarEspecialidadeCommand
            {
                Id = 99,
                Nome = "Ortopedia"
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Especialidade?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
