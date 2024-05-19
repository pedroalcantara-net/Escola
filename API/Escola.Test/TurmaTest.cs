using Escola.Application.Contract.Turma;
using Escola.Application.Service;
using Escola.Domain.Entity;
using Escola.Domain.Error;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;
using Moq;

namespace Escola.Test
{
    public class TurmaTest
    {
        private readonly Mock<ITurmaRepository> _turmaRepository = new();
        private readonly Mock<IAlunoTurmaRepository> _alunoTurmaRepository = new();
        private readonly TurmaService _turmaService;

        public TurmaTest()
        {
            _turmaService = new TurmaService(_turmaRepository.Object, _alunoTurmaRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBadRequestException_WhenNomeExists()
        {
            var turmaRequest = new TurmaRequest { Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year };
            _turmaRepository.Setup(x => x.NomeExistsAsync(turmaRequest.Nome)).ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _turmaService.AddAsync(turmaRequest));
            Assert.Contains(DomainErrors.Turma.NomeExists.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBadRequestException_WhenAnoIsInvalid()
        {
            var turmaRequest = new TurmaRequest { Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year - 1 };
            _turmaRepository.Setup(x => x.NomeExistsAsync(turmaRequest.Nome)).ReturnsAsync(false);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _turmaService.AddAsync(turmaRequest));
            Assert.Contains(DomainErrors.Turma.InvalidAno.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldAddTurma_WhenRequestIsValid()
        {
            var turmaRequest = new TurmaRequest { Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year };
            var turma = new Turma { Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year };

            _turmaRepository.Setup(x => x.NomeExistsAsync(turmaRequest.Nome)).ReturnsAsync(false);
            _turmaRepository.Setup(x => x.AddAsync(It.IsAny<Turma>())).ReturnsAsync(turma);

            var result = await _turmaService.AddAsync(turmaRequest);

            Assert.NotNull(result);
            Assert.Equal(turma.Id, result.Id);
            _turmaRepository.Verify(x => x.AddAsync(It.IsAny<Turma>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldThrowNotFoundException_WhenTurmaNotFound()
        {
            int turmaId = 1;
            _turmaRepository.Setup(x => x.GetByIdAsync(turmaId)).ReturnsAsync((Turma)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _turmaService.DeleteByIdAsync(turmaId));
            Assert.Contains(DomainErrors.Turma.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldCallRepositoryDelete_WhenTurmaExists()
        {
            int turmaId = 1;
            var turma = new Turma { Id = turmaId };
            _turmaRepository.Setup(x => x.GetByIdAsync(turmaId)).ReturnsAsync(turma);

            await _turmaService.DeleteByIdAsync(turmaId);

            _turmaRepository.Verify(x => x.DeleteByIdAsync(turmaId), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldThrowNotFoundException_WhenNoTurmasFound()
        {
            _turmaRepository.Setup(x => x.GetAsync()).ReturnsAsync(new List<Turma>());

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _turmaService.GetAsync());
            Assert.Contains(DomainErrors.Turma.NoneFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task GetAsync_ShouldReturnTurmaResponses_WhenTurmasFound()
        {
            var turmas = new List<Turma>
            {
            new(){ Id = 1, Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year },
            new(){ Id = 2, Nome = "Turma 2", CursoId = 2, Ano = DateTime.Now.Year }
            };
            _turmaRepository.Setup(x => x.GetAsync()).ReturnsAsync(turmas);

            var result = await _turmaService.GetAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Turma 1", result.First().Nome);
            Assert.Equal("Turma 2", result.Last().Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenTurmaNotFound()
        {
            int turmaId = 1;
            _turmaRepository.Setup(x => x.GetByIdAsync(turmaId)).ReturnsAsync((Turma)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _turmaService.GetByIdAsync(turmaId));
            Assert.Contains(DomainErrors.Turma.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTurmaResponse_WhenTurmaFound()
        {
            int turmaId = 1;
            var turma = new Turma { Id = turmaId, Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year };
            _turmaRepository.Setup(x => x.GetByIdAsync(turmaId)).ReturnsAsync(turma);

            var result = await _turmaService.GetByIdAsync(turmaId);

            Assert.NotNull(result);
            Assert.Equal(turmaId, result.Id);
            Assert.Equal("Turma 1", result.Nome);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException_WhenTurmaNotFound()
        {
            var turmaRequest = new TurmaRequest { Id = 1, Nome = "Updated Nome", CursoId = 1, Ano = DateTime.Now.Year };
            _turmaRepository.Setup(x => x.GetByIdAsync(turmaRequest.Id.Value)).ReturnsAsync((Turma)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _turmaService.UpdateAsync(turmaRequest));
            Assert.Contains(DomainErrors.Turma.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTurma_WhenRequestIsValid()
        {
            var turmaRequest = new TurmaRequest { Id = 1, Nome = "Updated Nome", CursoId = 1, Ano = DateTime.Now.Year };
            var turma = new Turma { Id = turmaRequest.Id.Value, Nome = "Updated Nome", CursoId = 1, Ano = DateTime.Now.Year };
            _turmaRepository.Setup(x => x.GetByIdAsync(turmaRequest.Id.Value)).ReturnsAsync(turma);
            _turmaRepository.Setup(x => x.UpdateAsync(turma)).ReturnsAsync(turma);

            var result = await _turmaService.UpdateAsync(turmaRequest);

            Assert.NotNull(result);
            Assert.Equal(turmaRequest.Id.Value, result.Id);
            Assert.Equal(turmaRequest.Nome, result.Nome);
            _turmaRepository.Verify(x => x.UpdateAsync(turma), Times.Once);
        }

        [Fact]
        public async Task GetByAlunoIdAsync_ShouldThrowNotFoundException_WhenNoTurmasFound()
        {

            int alunoId = 1;
            _turmaRepository.Setup(x => x.GetByAlunoIdAsync(alunoId)).ReturnsAsync(new List<Turma>());

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _turmaService.GetByAlunoIdAsync(alunoId));
            Assert.Contains(DomainErrors.Turma.NoneFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task GetByAlunoIdAsync_ShouldReturnTurmaResponses_WhenTurmasFound()
        {
            int alunoId = 1;
            var turmas = new List<Turma>
            {
            new(){ Id = 1, Nome = "Turma 1", CursoId = 1, Ano = DateTime.Now.Year },
            new(){ Id = 2, Nome = "Turma 2", CursoId = 2, Ano = DateTime.Now.Year }
            };
            _turmaRepository.Setup(x => x.GetByAlunoIdAsync(alunoId)).ReturnsAsync(turmas);

            var result = await _turmaService.GetByAlunoIdAsync(alunoId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Turma 1", result.First().Nome);
            Assert.Equal("Turma 2", result.Last().Nome);
        }
    }
}
