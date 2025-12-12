using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Commands;
using ClinicaPro.Core.Features.Especialidades.Handlers;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Core.Features.Especialidades.Commands
{
    public class CriarEspecialidadeCommandHandlerTests
    {
        private readonly Mock<IEspecialidadeRepository> _repositoryMock;
        private readonly CriarEspecialidadeCommandHandler _handler;

        public CriarEspecialidadeCommandHandlerTests()
        {
            _repositoryMock = new Mock<IEspecialidadeRepository>();
            _handler = new CriarEspecialidadeCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Especialidade_Com_Sucesso()
        {
            // Arrange
            var command = new CriarEspecialidadeCommand
            {
                Nome = "Cardiologia"
            };

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Especialidade>()))
                .ReturnsAsync((Especialidade e) => { e.Id = 1; return e.Id; });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Cardiologia", result.Nome);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Especialidade>()), Times.Once);
        }
    }
}
