using Escola.Domain.Entity;

namespace Escola.Domain.Interface.Repository
{
    public interface IAlunoTurmaRepository
    {
        IEnumerable<AlunoTurma> GetByTurmaId(int turmaId);
        IEnumerable<AlunoTurma> GetByAlunoId(int alunoId);
        AlunoTurma Add(AlunoTurma Turma);
        void Delete(int turmaId, int alunoId);
    }
}
