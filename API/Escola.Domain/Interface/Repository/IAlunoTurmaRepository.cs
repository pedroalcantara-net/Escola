using Escola.Domain.Entity;

namespace Escola.Domain.Interface.Repository
{
    public interface IAlunoTurmaRepository
    {
        Task<bool> AlunoTurmaExistsAsync(int alunoId, int turmaId);
        Task<AlunoTurma> AddAsync(AlunoTurma Turma);
        Task DeleteAsync(int alunoId, int turmaId);
    }
}
