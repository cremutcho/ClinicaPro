using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using Moq;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetAllFuncionarios;

namespace ClinicaPro.Tests.Core.Features.Funcionarios
{
    public class GetAllFuncionariosQueryHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _repositoryMock;
        private readonly GetAllFuncionariosQueryHandler _handler;

        public GetAllFuncionariosQueryHandlerTests()
        {
            _repositoryMock = new Mock<IFuncionarioRepository>();
            _handler = new GetAllFuncionariosQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Funcionarios()
        {
            // Arrange
            var funcionariosFake = new List<Funcionario>
            {
                new Funcionario { Id = 1, Nome = "Ana", CargoId = 2, Ativo = true },
                new Funcionario { Id = 2, Nome = "Bruno", CargoId = 3, Ativo = true }
            };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(funcionariosFake);

            var query = new GetAllFuncionariosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Funcionario>)result).Count);

            Assert.Equal("Ana", ((List<Funcionario>)result)[0].Nome);
            Assert.Equal("Bruno", ((List<Funcionario>)result)[1].Nome);

            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Funcionarios()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Funcionario>());

            var query = new GetAllFuncionariosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var lista = Assert.IsType<List<Funcionario>>(result);
            Assert.Empty(lista);

            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
