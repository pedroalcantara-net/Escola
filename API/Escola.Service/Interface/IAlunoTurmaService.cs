using Escola.Application.Contract.AlunoTurma;

namespace Escola.Application.Interface
{
    public interface IAlunoTurmaService
    {
        Task<AlunoTurmaResponse> AddAsync(AlunoTurmaRequest Turma);
        Task DeleteAsync(int alunoId, int turmaId);
    }
}
