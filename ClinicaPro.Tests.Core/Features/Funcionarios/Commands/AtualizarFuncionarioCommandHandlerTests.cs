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
    public class AtualizarFuncionarioCommandHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly AtualizarFuncionarioCommandHandler _handler;

        public AtualizarFuncionarioCommandHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new AtualizarFuncionarioCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Atualizar_Funcionario_Com_Sucesso()
        {
            // Arrange
            var funcionarioExistente = new Funcionario
            {
                Id = 5,
                Nome = "Maria Antiga",
                CargoId = 1,
                Ativo = true
            };

            var command = new AtualizarFuncionarioCommand
            {
                Id = 5,
                Nome = "Maria Atualizada",
                CargoId = 2,
                Ativo = false
            };

            // quando GetByIdAsync for chamado, retorna o funcionário existente
            _repositoryMock
                .Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(funcionarioExistente);

            // captura o funcionário passado para UpdateAsync para inspecionar os valores atualizados
            Funcionario? funcionarioAtualizado = null;
            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Funcionario>()))
                .Callback<Funcionario>(f => funcionarioAtualizado = f)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            // O handler retorna bool indicando sucesso
            Assert.True(result);

            // Verifica que o UpdateAsync foi chamado e que o objeto recebido contém as alterações
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Funcionario>()), Times.Once);
            Assert.NotNull(funcionarioAtualizado);
            Assert.Equal(5, funcionarioAtualizado!.Id); // mantém o id
            Assert.Equal("Maria Atualizada", funcionarioAtualizado.Nome);
            Assert.Equal(2, funcionarioAtualizado.CargoId);
            Assert.False(funcionarioAtualizado.Ativo);
        }

        [Fact]
        public async Task Deve_Retornar_False_Se_Funcionario_Nao_Existir()
        {
            // Arrange
            var command = new AtualizarFuncionarioCommand
            {
                Id = 999,
                Nome = "Inexistente",
                CargoId = 1,
                Ativo = true
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
