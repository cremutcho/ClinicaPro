using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionarioById;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Features.RH.Funcionarios.Queries
{
    public class GetFuncionarioByIdQueryHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly GetFuncionarioByIdQueryHandler _handler;

        public GetFuncionarioByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new GetFuncionarioByIdQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFuncionario_QuandoEncontrado()
        {
            // Arrange
            var funcionario = new Funcionario
            {
                Id = 1,
                Nome = "João Andrade",
                CargoId = 2, // usa o FK em vez de Cargo.Auxiliar
                Ativo = true
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(1))
                           .ReturnsAsync(funcionario);

            var query = new GetFuncionarioByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("João Andrade", result.Nome);
            Assert.Equal(2, result.CargoId); // verifica o cargo via FK
            Assert.True(result.Ativo);
        }

        [Fact]
        public async Task Handle_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(99))
                           .ReturnsAsync((Funcionario?)null);

            var query = new GetFuncionarioByIdQuery(99);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
