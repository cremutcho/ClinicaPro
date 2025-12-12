using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorPeriodo;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Features.RH.Funcionarios.Queries
{
    public class GetFuncionariosPorPeriodoQueryHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly GetFuncionariosPorPeriodoQueryHandler _handler;

        public GetFuncionariosPorPeriodoQueryHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new GetFuncionariosPorPeriodoQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFuncionariosNoPeriodo_QuandoExistirem()
        {
            // Arrange
            var inicio = new DateTime(2023, 1, 1);
            var fim = new DateTime(2023, 12, 31);

            // Não usamos DataContratacao porque sua entidade não possui essa propriedade.
            // Mockamos a resposta que o repositório deve devolver quando chamado com esse período.
            var funcionarios = new List<Funcionario>
            {
                new Funcionario { Id = 1, Nome = "Ana", CargoId = 2, Ativo = true },
                new Funcionario { Id = 2, Nome = "Bruno", CargoId = 4, Ativo = true }
            };

            _repositoryMock
                .Setup(r => r.BuscarPorPeriodoAsync(inicio, fim))
                .ReturnsAsync(funcionarios);

            var query = new GetFuncionariosPorPeriodoQuery(inicio, fim);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            // Verifica que os itens retornados são os mesmos mockados
            Assert.Contains(result, f => f.Id == 1 && f.Nome == "Ana");
            Assert.Contains(result, f => f.Id == 2 && f.Nome == "Bruno");
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoExistiremFuncionariosNoPeriodo()
        {
            // Arrange
            var inicio = new DateTime(2020, 1, 1);
            var fim = new DateTime(2020, 12, 31);

            _repositoryMock
                .Setup(r => r.BuscarPorPeriodoAsync(inicio, fim))
                .ReturnsAsync(new List<Funcionario>());

            var query = new GetFuncionariosPorPeriodoQuery(inicio, fim);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
