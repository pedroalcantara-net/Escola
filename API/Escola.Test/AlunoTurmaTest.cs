using Escola.Application.Contract.AlunoTurma;
using Escola.Application.Service;
using Escola.Domain.Entity;
using Escola.Domain.Error;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;
using Moq;

namespace Escola.Test
{
    public class AlunoTurmaTest
    {
        private readonly Mock<IAlunoTurmaRepository> _alunoTurmaRepository = new();
        private readonly Mock<IAlunoRepository> _alunoRepository = new();
        private readonly Mock<ITurmaRepository> _turmaRepository = new();
        private readonly AlunoTurmaService _alunoTurmaService;

        public AlunoTurmaTest()
        {
            _alunoTurmaService = new AlunoTurmaService(_alunoTurmaRepository.Object, _alunoRepository.Object, _turmaRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowNotFoundException_WhenAlunoNotFound()
        {
            var alunoTurmaRequest = new AlunoTurmaRequest { AlunoId = 1, TurmaId = 1 };
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.AlunoId)).ReturnsAsync((Aluno)null);
            _turmaRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.TurmaId)).ReturnsAsync(new Turma());

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoTurmaService.AddAsync(alunoTurmaRequest));
            Assert.Contains(DomainErrors.Aluno.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldThrowNotFoundException_WhenTurmaNotFound()
        {
            var alunoTurmaRequest = new AlunoTurmaRequest { AlunoId = 1, TurmaId = 1 };
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.AlunoId)).ReturnsAsync(new Aluno());
            _turmaRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.TurmaId)).ReturnsAsync((Turma)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoTurmaService.AddAsync(alunoTurmaRequest));
            Assert.Contains(DomainErrors.Turma.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBadRequestException_WhenAlunoTurmaAlreadyExists()
        {
            var alunoTurmaRequest = new AlunoTurmaRequest { AlunoId = 1, TurmaId = 1 };
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.AlunoId)).ReturnsAsync(new Aluno());
            _turmaRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.TurmaId)).ReturnsAsync(new Turma());
            _alunoTurmaRepository.Setup(x => x.AlunoTurmaExistsAsync(alunoTurmaRequest.AlunoId, alunoTurmaRequest.TurmaId)).ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _alunoTurmaService.AddAsync(alunoTurmaRequest));
            Assert.Contains(DomainErrors.AlunoTurma.AlreadyExists.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldAddAlunoTurma_WhenRequestIsValid()
        {
            var alunoTurmaRequest = new AlunoTurmaRequest { AlunoId = 1, TurmaId = 1 };
            var alunoTurma = new AlunoTurma { AlunoId = alunoTurmaRequest.AlunoId, TurmaId = alunoTurmaRequest.TurmaId };

            _alunoRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.AlunoId)).ReturnsAsync(new Aluno());
            _turmaRepository.Setup(x => x.GetByIdAsync(alunoTurmaRequest.TurmaId)).ReturnsAsync(new Turma());
            _alunoTurmaRepository.Setup(x => x.AlunoTurmaExistsAsync(alunoTurmaRequest.AlunoId, alunoTurmaRequest.TurmaId)).ReturnsAsync(false);
            _alunoTurmaRepository.Setup(x => x.AddAsync(It.IsAny<AlunoTurma>())).ReturnsAsync(alunoTurma);

            var result = await _alunoTurmaService.AddAsync(alunoTurmaRequest);

            Assert.NotNull(result);
            Assert.Equal(alunoTurmaRequest.AlunoId, result.AlunoId);
            Assert.Equal(alunoTurmaRequest.TurmaId, result.TurmaId);
            _alunoTurmaRepository.Verify(x => x.AddAsync(It.IsAny<AlunoTurma>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowBadRequestException_WhenAlunoTurmaNotFound()
        {
            int alunoId = 1;
            int turmaId = 1;
            _alunoTurmaRepository.Setup(x => x.AlunoTurmaExistsAsync(alunoId, turmaId)).ReturnsAsync(false);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _alunoTurmaService.DeleteAsync(alunoId, turmaId));
            Assert.Contains(DomainErrors.AlunoTurma.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallDelete_WhenAlunoTurmaExists()
        {
            int alunoId = 1;
            int turmaId = 1;
            _alunoTurmaRepository.Setup(x => x.AlunoTurmaExistsAsync(alunoId, turmaId)).ReturnsAsync(true);

            await _alunoTurmaService.DeleteAsync(alunoId, turmaId);

            _alunoTurmaRepository.Verify(x => x.DeleteAsync(alunoId, turmaId), Times.Once);
        }
    }
}
