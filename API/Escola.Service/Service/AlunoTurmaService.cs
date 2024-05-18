using Escola.Application.Contract.AlunoTurma;
using Escola.Application.Interface;
using Escola.Domain.Entity;
using Escola.Domain.Error;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;

namespace Escola.Application.Service
{
    public class AlunoTurmaService(IAlunoTurmaRepository alunoTurmaRepository, IAlunoRepository alunoRepository, ITurmaRepository turmaRepository) : IAlunoTurmaService
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly ITurmaRepository _turmaRepository = turmaRepository;
        private readonly List<Erro> _erros = [];

        public async Task<AlunoTurmaResponse> AddAsync(AlunoTurmaRequest alunoTurmaRequest)
        {
            if (await _alunoRepository.GetByIdAsync(alunoTurmaRequest.AlunoId) is null) _erros.Add(DomainErrors.Aluno.NotFound);
            if (await _turmaRepository.GetByIdAsync(alunoTurmaRequest.TurmaId) is null) _erros.Add(DomainErrors.Turma.NotFound);
            if (_erros.Count != 0) throw new NotFoundException(_erros);

            if (await _alunoTurmaRepository.AlunoTurmaExistsAsync(alunoTurmaRequest.AlunoId, alunoTurmaRequest.TurmaId)) throw new BadRequestException(DomainErrors.AlunoTurma.AlreadyExists);

            var alunoTurma = new AlunoTurma()
            {
                AlunoId = alunoTurmaRequest.AlunoId,
                TurmaId = alunoTurmaRequest.TurmaId,
            };

            alunoTurma = await _alunoTurmaRepository.AddAsync(alunoTurma);

            return new AlunoTurmaResponse(alunoTurma);
        }

        public async Task DeleteAsync(int alunoId, int turmaId)
        {
            if (!await _alunoTurmaRepository.AlunoTurmaExistsAsync(alunoId, turmaId)) throw new BadRequestException(DomainErrors.AlunoTurma.NotFound);

            await _alunoTurmaRepository.DeleteAsync(alunoId, turmaId);
        }

    }
}
