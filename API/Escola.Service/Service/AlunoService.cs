using Escola.Application.Contract.Aluno;
using Escola.Application.Interface;
using Escola.Domain.Core.Utility;
using Escola.Domain.Entity;
using Escola.Domain.Error;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;

namespace Escola.Application.Service
{
    public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;

        private readonly List<Erro> _erros = [];

        public async Task<AlunoResponse> AddAsync(AlunoRequest alunoRequest)
        {
            if (await _alunoRepository.UsuarioExistsAsync(alunoRequest.Usuario)) _erros.Add(DomainErrors.Aluno.UsuarioExists);

            if (alunoRequest.Senha != alunoRequest.SenhaConfirmacao) _erros.Add(DomainErrors.Aluno.SenhaDoesNotMatch);

            if (_erros.Count != 0) throw new BadRequestException(_erros);

            string senhaEncrypted = Cryptography.GetSha256(alunoRequest.Senha);

            var aluno = new Aluno()
            {
                Nome = alunoRequest.Nome,
                Usuario = alunoRequest.Usuario,
                Senha = senhaEncrypted
            };

            aluno = await _alunoRepository.AddAsync(aluno);

            return new AlunoResponse(aluno);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _ = await _alunoRepository.GetByIdAsync(id) ?? throw new NotFoundException(DomainErrors.Aluno.NotFound);

            await _alunoRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<AlunoResponse>> GetAsync()
        {
            var alunos = await _alunoRepository.GetAsync();

            if (!alunos.Any()) throw new NotFoundException(DomainErrors.Aluno.NoneFound);

            return alunos.Select(x => new AlunoResponse(x)).ToList();
        }

        public async Task<AlunoResponse> GetByIdAsync(int id)
        {
            var aluno = await _alunoRepository.GetByIdAsync(id) ?? throw new NotFoundException(DomainErrors.Aluno.NotFound);

            return new AlunoResponse(aluno);
        }

        public async Task<AlunoResponse> UpdateAsync(AlunoRequest alunoRequest)
        {
            var aluno = await _alunoRepository.GetByIdAsync(alunoRequest.Id) ?? throw new NotFoundException(DomainErrors.Aluno.NotFound);

            if (alunoRequest.Senha is not null && alunoRequest.Senha != alunoRequest.SenhaConfirmacao) _erros.Add(DomainErrors.Aluno.SenhaDoesNotMatch);

            if (_erros.Count != 0) throw new BadRequestException(_erros);

            aluno.Nome = alunoRequest.Nome;
            if (alunoRequest.Senha is not null) aluno.Senha = Cryptography.GetSha256(alunoRequest.Senha);

            await _alunoRepository.UpdateAsync(aluno);

            return new AlunoResponse(aluno);
        }

        public async Task<IEnumerable<AlunoResponse>> GetByTurmaIdAsync(int turmaId)
        {
            var alunoTurma = await _alunoRepository.GetByTurmaIdAsync(turmaId);

            if (!alunoTurma.Any()) throw new NotFoundException(DomainErrors.Aluno.NoneFound);

            return alunoTurma.Select(x => new AlunoResponse(x));
        }
    }
}
