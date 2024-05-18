using Escola.Application.Contract.Aluno;

namespace Escola.Application.Interface
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoResponse>> GetAsync();
        Task<AlunoResponse> GetByIdAsync(int id);
        Task<AlunoResponse> AddAsync(AlunoRequest aluno);
        Task<AlunoResponse> UpdateAsync(AlunoRequest aluno);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<AlunoResponse>> GetByTurmaIdAsync(int turmaId);
    }
}
