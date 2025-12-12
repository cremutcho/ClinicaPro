using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorStatus;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Features.RH.Funcionarios.Queries
{
    public class GetFuncionariosPorStatusQueryHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly GetFuncionariosPorStatusQueryHandler _handler;

        public GetFuncionariosPorStatusQueryHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new GetFuncionariosPorStatusQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFuncionariosAtivos_QuandoExistirem()
        {
            // Arrange
            var funcionarios = new List<Funcionario>
            {
                new Funcionario { Id = 1, Nome = "Ana", CargoId = 2, Ativo = true },
                new Funcionario { Id = 2, Nome = "Bruno", CargoId = 4, Ativo = true }
            };

            _repositoryMock
                .Setup(r => r.BuscarPorStatusAsync(true))
                .ReturnsAsync(funcionarios);

            var query = new GetFuncionariosPorStatusQuery(true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, f => Assert.True(f.Ativo));
        }

        [Fact]
        public async Task Handle_DeveRetornarFuncionariosInativos_QuandoExistirem()
        {
            // Arrange
            var funcionarios = new List<Funcionario>
            {
                new Funcionario { Id = 3, Nome = "Carlos", CargoId = 5, Ativo = false }
            };

            _repositoryMock
                .Setup(r => r.BuscarPorStatusAsync(false))
                .ReturnsAsync(funcionarios);

            var query = new GetFuncionariosPorStatusQuery(false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.False(result.First().Ativo);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHouverFuncionariosComStatus()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.BuscarPorStatusAsync(false))
                .ReturnsAsync(new List<Funcionario>());

            var query = new GetFuncionariosPorStatusQuery(false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
