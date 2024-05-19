using Escola.Domain.Entity;

namespace Escola.Domain.Interface.Repository
{
    public interface IAlunoTurmaRepository
    {
        Task<IEnumerable<AlunoTurma>> GetByAlunoId(int alunoId);
        Task<IEnumerable<AlunoTurma>> GetByTurmaId(int turmaId);
        Task<bool> AlunoTurmaExistsAsync(int alunoId, int turmaId);
        Task<AlunoTurma> AddAsync(AlunoTurma Turma);
        Task DeleteAsync(int alunoId, int turmaId);
    }
}
