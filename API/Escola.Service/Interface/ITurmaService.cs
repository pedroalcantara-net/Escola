using Escola.Application.Contract.Turma;

namespace Escola.Application.Interface
{
    public interface ITurmaService
    {
        Task<IEnumerable<TurmaResponse>> GetAsync();
        Task<TurmaResponse> GetByIdAsync(int id);
        Task<TurmaResponse> AddAsync(TurmaRequest Turma);
        Task<TurmaResponse> UpdateAsync(TurmaRequest Turma);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<TurmaResponse>> GetByAlunoIdAsync(int alunoId);
    }
}
