using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Queries;
using ClinicaPro.Core.Interfaces;
using Moq;
using Xunit;

namespace ClinicaPro.Tests.Core.Features.Especialidades.Queries
{
    public class ObterTodasEspecialidadesQueryHandlerTests
    {
        private readonly Mock<IEspecialidadeRepository> _repositoryMock;
        private readonly ObterTodasEspecialidadesQueryHandler _handler;

        public ObterTodasEspecialidadesQueryHandlerTests()
        {
            _repositoryMock = new Mock<IEspecialidadeRepository>();
            _handler = new ObterTodasEspecialidadesQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarTodasEspecialidades()
        {
            // Arrange
            var especialidades = new List<Especialidade>
            {
                new Especialidade { Id = 1, Nome = "Cardiologia" },
                new Especialidade { Id = 2, Nome = "Dermatologia" }
            };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(especialidades);

            var query = new ObterTodasEspecialidadesQuery();

            // Act
            var result = (await _handler.Handle(query, CancellationToken.None)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Nome == "Cardiologia");
            Assert.Contains(result, e => e.Nome == "Dermatologia");
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoExistiremEspecialidades()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Especialidade>());

            var query = new ObterTodasEspecialidadesQuery();

            // Act
            var result = (await _handler.Handle(query, CancellationToken.None)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
