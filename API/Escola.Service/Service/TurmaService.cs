using Escola.Application.Contract.Turma;
using Escola.Application.Interface;
using Escola.Domain.Entity;
using Escola.Domain.Error;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;

namespace Escola.Application.Service
{
    public class TurmaService(ITurmaRepository turmaRepository, IAlunoTurmaRepository alunoTurmaRepository) : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository = turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;
        private readonly List<Erro> _erros = [];

        public async Task<TurmaResponse> AddAsync(TurmaRequest turmaRequest)
        {
            if (await _turmaRepository.NomeExistsAsync(turmaRequest.Nome)) _erros.Add(DomainErrors.Turma.NomeExists);

            if (turmaRequest.Ano < DateTime.Now.Year) _erros.Add(DomainErrors.Turma.InvalidAno);

            if (_erros.Count != 0) throw new BadRequestException(_erros);

            var turma = new Turma()
            {
                Nome = turmaRequest.Nome,
                CursoId = turmaRequest.CursoId ?? 0,
                Ano = turmaRequest.Ano ?? DateTime.Now.Year
            };

            turma = await _turmaRepository.AddAsync(turma);

            return new TurmaResponse(turma);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _ = await _turmaRepository.GetByIdAsync(id) ?? throw new NotFoundException(DomainErrors.Turma.NotFound);

            var alunos = await _alunoTurmaRepository.GetByAlunoId(id);
            if (alunos.Any()) throw new BadRequestException(DomainErrors.Turma.HasAluno);

            await _turmaRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<TurmaResponse>> GetAsync()
        {
            var turmas = await _turmaRepository.GetAsync();

            if (!turmas.Any()) throw new NotFoundException(DomainErrors.Turma.NoneFound);

            return turmas.Select(x => new TurmaResponse(x)).ToList();
        }

        public async Task<TurmaResponse> GetByIdAsync(int id)
        {
            var turma = await _turmaRepository.GetByIdAsync(id) ?? throw new NotFoundException(DomainErrors.Turma.NotFound);

            return new TurmaResponse(turma);
        }

        public async Task<TurmaResponse> UpdateAsync(TurmaRequest turmaRequest)
        {
            var turma = await _turmaRepository.GetByIdAsync(turmaRequest.Id ?? 0) ?? throw new NotFoundException(DomainErrors.Turma.NotFound);

            if (turmaRequest.Nome != turma.Nome && await _turmaRepository.NomeExistsAsync(turmaRequest.Nome)) _erros.Add(DomainErrors.Turma.NomeExists);
            if (turmaRequest.Ano != turma.Ano && turmaRequest.Ano < DateTime.Now.Year) _erros.Add(DomainErrors.Turma.InvalidAno);

            if (_erros.Count != 0) throw new BadRequestException(_erros);

            await _turmaRepository.UpdateAsync(turma);

            return new TurmaResponse(turma);
        }


        public async Task<IEnumerable<TurmaResponse>> GetByAlunoIdAsync(int alunoId)
        {
            var turma = await _turmaRepository.GetByAlunoIdAsync(alunoId);

            if (!turma.Any()) throw new NotFoundException(DomainErrors.Turma.NoneFound);

            return turma.Select(x => new TurmaResponse(x));
        }
    }
}
