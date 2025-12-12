using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.RH.Funcionarios.Commands;
using ClinicaPro.Core.Features.RH.Funcionarios.Handlers;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Core.Features.Funcionarios
{
    public class AlterarStatusFuncionarioCommandHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly AlterarStatusFuncionarioCommandHandler _handler;

        public AlterarStatusFuncionarioCommandHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new AlterarStatusFuncionarioCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Alterar_Status_Do_Funcionario_Com_Sucesso()
        {
            // Arrange
            var funcionario = new Funcionario
            {
                Id = 10,
                Nome = "Carlos",
                CargoId = 3,
                Ativo = true
            };

            var command = new AlterarStatusFuncionarioCommand
            {
                Id = 10,
                NovoStatusAtivo = false // propriedade correta ✔
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(10))
                .ReturnsAsync(funcionario);

            Funcionario? funcionarioAtualizado = null;

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Funcionario>()))
                .Callback<Funcionario>(f => funcionarioAtualizado = f)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);

            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Funcionario>()), Times.Once);

            Assert.NotNull(funcionarioAtualizado);
            Assert.Equal(10, funcionarioAtualizado!.Id);
            Assert.False(funcionarioAtualizado.Ativo); // ✔ deve alterar
        }

        [Fact]
        public async Task Deve_Retornar_False_Se_Funcionario_Nao_Existir()
        {
            // Arrange
            var command = new AlterarStatusFuncionarioCommand
            {
                Id = 999,
                NovoStatusAtivo = true // ✔
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Funcionario?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Funcionario>()), Times.Never);
        }
    }
}
