using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorCargo;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Features.RH.Funcionarios.Queries
{
    public class GetFuncionariosPorCargoQueryHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly GetFuncionariosPorCargoQueryHandler _handler;

        public GetFuncionariosPorCargoQueryHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new GetFuncionariosPorCargoQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFuncionarios_DoCargo_QuandoExistirem()
        {
            // Arrange
            int cargoId = 3;

            var funcionarios = new List<Funcionario>
            {
                new Funcionario { Id = 1, Nome = "Ana", CargoId = 3, Ativo = true },
                new Funcionario { Id = 2, Nome = "Marcos", CargoId = 3, Ativo = false }
            };

            _repositoryMock
                .Setup(r => r.BuscarPorCargoAsync(cargoId))
                .ReturnsAsync(funcionarios);

            var query = new GetFuncionariosPorCargoQuery(cargoId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());          // <-- usa Count()
            Assert.All(result, f => Assert.Equal(cargoId, f.CargoId));
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoExistiremFuncionarios()
        {
            // Arrange
            int cargoId = 999;

            _repositoryMock
                .Setup(r => r.BuscarPorCargoAsync(cargoId))
                .ReturnsAsync(new List<Funcionario>());

            var query = new GetFuncionariosPorCargoQuery(cargoId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
