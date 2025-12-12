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
    public class CriarFuncionarioCommandHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly CriarFuncionarioCommandHandler _handler;

        public CriarFuncionarioCommandHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new CriarFuncionarioCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Funcionario_E_Retornar_Id()
        {
            // Arrange
            var command = new CriarFuncionarioCommand();
            // não atribuí propriedades desconhecidas (Ativo, CargoId, etc.)
            // se seu command exigir propriedades obrigatórias no construtor,
            // cole aqui as propriedades reais (vou mostrar como adaptar logo abaixo).

            // Simula o comportamento do repositório: quando AddAsync for chamado,
            // atribui Id ao objeto e completa a task (já que AddAsync retorna Task).
            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Funcionario>()))
                .Callback<Funcionario>(f =>
                {
                    f.Id = 10; // simula id atribuído pelo banco
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            // Aqui assumimos que o handler devolve o ID (int). Por isso:
            Assert.Equal(10, result);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Funcionario>()), Times.Once);
        }
    }
}
