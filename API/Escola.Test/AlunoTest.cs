using Escola.Application.Contract.Aluno;
using Escola.Application.Interface;
using Escola.Application.Service;
using Escola.Domain.Core.Utility;
using Escola.Domain.Entity;
using Escola.Domain.Error;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;
using Moq;

namespace Escola.Test
{
    public class AlunoTest
    {

        private readonly Mock<IAlunoRepository> _alunoRepository = new();
        private readonly Mock<IAlunoTurmaRepository> _alunoTurmaRepository = new();
        private IAlunoService _alunoService;

        public AlunoTest()
        {
            _alunoService = new AlunoService(_alunoRepository.Object, _alunoTurmaRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBadRequestException_WhenUsuarioExists()
        {
            var alunoRequest = SetupAlunoRequest();

            _alunoRepository.Setup(x => x.UsuarioExistsAsync(alunoRequest.Usuario)).ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _alunoService.AddAsync(alunoRequest));
            Assert.Contains(DomainErrors.Aluno.UsuarioExists.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBadRequestException_WhenSenhasDoNotMatch()
        {
            var alunoRequest = SetupAlunoRequest();
            alunoRequest.SenhaConfirmacao = "OutraSenhaQualquer";

            _alunoRepository.Setup(x => x.UsuarioExistsAsync(alunoRequest.Usuario)).ReturnsAsync(false);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _alunoService.AddAsync(alunoRequest));
            Assert.Contains(DomainErrors.Aluno.SenhaDoesNotMatch.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task AddAsync_ShouldAdd_WhenRequestIsValid()
        {
            var alunoRequest = SetupAlunoRequest();
            alunoRequest.SenhaConfirmacao = alunoRequest.Senha;

            var aluno = SetupAluno();

            _alunoRepository.Setup(x => x.UsuarioExistsAsync(alunoRequest.Usuario)).ReturnsAsync(false);
            _alunoRepository.Setup(x => x.AddAsync(It.IsAny<Aluno>())).ReturnsAsync(aluno);

            var response = await _alunoService.AddAsync(alunoRequest);

            Assert.NotNull(response);
            Assert.Equal(alunoRequest.Nome, response.Nome);
            Assert.Equal(alunoRequest.Usuario, response.Usuario);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldThrowNotFoundException_WhenAlunoNotFound()
        {
            int alunoId = 1;
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoId)).ReturnsAsync((Aluno)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoService.DeleteByIdAsync(alunoId));
            Assert.Contains(DomainErrors.Aluno.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldCallDelete_WhenAlunoExists()
        {
            var aluno = SetupAluno();
            _alunoRepository.Setup(x => x.GetByIdAsync(aluno.Id)).ReturnsAsync(aluno);

            await _alunoService.DeleteByIdAsync(aluno.Id);

            _alunoRepository.Verify(x => x.DeleteByIdAsync(aluno.Id), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldThrowNotFoundException_WhenNoneFound()
        {
            _alunoRepository.Setup(x => x.GetAsync()).ReturnsAsync(new List<Aluno>());

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoService.GetAsync());
            Assert.Contains(DomainErrors.Aluno.NoneFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAlunoResponseList_WhenAlunosFound()
        {
            var alunos = new List<Aluno>
            {
            SetupAluno(),
            SetupAluno()
            };

            _alunoRepository.Setup(x => x.GetAsync()).ReturnsAsync(alunos);


            var result = await _alunoService.GetAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Aluno de Teste", result.First().Nome);
            Assert.Equal("Aluno de Teste", result.Last().Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenAlunoNotFound()
        {
            int alunoId = 1;
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoId)).ReturnsAsync((Aluno)null);


            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoService.GetByIdAsync(alunoId));
            Assert.Contains(DomainErrors.Aluno.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAlunoResponse_WhenAlunoFound()
        {
            var aluno = SetupAluno();
            _alunoRepository.Setup(x => x.GetByIdAsync(aluno.Id)).ReturnsAsync(aluno);

            var result = await _alunoService.GetByIdAsync(aluno.Id);

            Assert.NotNull(result);
            Assert.Equal(aluno.Id, result.Id);
            Assert.Equal(aluno.Nome, result.Nome);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException_WhenAlunoNotFound()
        {
            var alunoRequest = SetupAlunoRequest();
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoRequest.Id.Value)).ReturnsAsync((Aluno)null);


            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoService.UpdateAsync(alunoRequest));
            Assert.Contains(DomainErrors.Aluno.NotFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowBadRequestException_WhenSenhasDoNotMatch()
        {
            var aluno = SetupAluno();
            var alunoRequest = SetupAlunoRequest();
            alunoRequest.SenhaConfirmacao = "OutraSenhaQualquer";

            _alunoRepository.Setup(x => x.GetByIdAsync(alunoRequest.Id.Value)).ReturnsAsync(aluno);


            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _alunoService.UpdateAsync(alunoRequest));
            Assert.Contains(DomainErrors.Aluno.SenhaDoesNotMatch.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAluno_WhenRequestIsValid()
        {
            var aluno = SetupAluno();
            var alunoRequest = SetupAlunoRequest();
            alunoRequest.SenhaConfirmacao = alunoRequest.Senha;
            _alunoRepository.Setup(x => x.GetByIdAsync(alunoRequest.Id.Value)).ReturnsAsync(aluno);
            _alunoRepository.Setup(x => x.UpdateAsync(aluno)).ReturnsAsync(aluno);


            var result = await _alunoService.UpdateAsync(alunoRequest);


            Assert.NotNull(result);
            Assert.Equal(alunoRequest.Nome, result.Nome);
            Assert.Equal(Cryptography.GetSha256(alunoRequest.Senha), aluno.Senha);
            _alunoRepository.Verify(x => x.UpdateAsync(aluno), Times.Once);
        }

        [Fact]
        public async Task GetByTurmaIdAsync_ShouldThrowNotFoundException_WhenNoneFound()
        {
            int turmaId = 1;

            _alunoRepository.Setup(x => x.GetByTurmaIdAsync(turmaId)).ReturnsAsync(new List<Aluno>());


            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _alunoService.GetByTurmaIdAsync(turmaId));
            Assert.Contains(DomainErrors.Aluno.NoneFound.Codigo, exception.Erros.Select(x => x.Codigo));
        }

        [Fact]
        public async Task GetByTurmaIdAsync_ShouldReturnAlunoResponseList_WhenAlunosFound()
        {
            int turmaId = 1;

            var alunos = new List<Aluno>
            {
            SetupAluno(),
            SetupAluno()
            };

            _alunoRepository.Setup(x => x.GetByTurmaIdAsync(turmaId)).ReturnsAsync(alunos);


            var result = await _alunoService.GetByTurmaIdAsync(turmaId);


            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Aluno de Teste", result.First().Nome);
            Assert.Equal("Aluno de Teste", result.Last().Nome);
        }

        private static AlunoRequest SetupAlunoRequest()
        {
            return new AlunoRequest
            {
                Id = 1,
                Nome = "Aluno de Teste",
                Usuario = "alunoteste123",
                Senha = "SenhaSegura123!"
            };
        }

        private static Aluno SetupAluno()
        {
            return new Aluno
            {
                Id = 1,
                Nome = "Aluno de Teste",
                Usuario = "alunoteste123",
                Senha = "27ab492d42888770e100ebb6b0d7a0e4f6c3102eceb22bbf34c35aa76d4fdd0a"
            };
        }
    }
}