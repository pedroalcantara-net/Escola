using Escola.Domain.Entity;

namespace Escola.Domain.Interface.Repository
{
    public interface ITurmaRepository
    {
        Task<Turma> AddAsync(Turma aluno);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<Turma>> GetAsync();
        Task<Turma> GetByIdAsync(int id);
        Task<Turma> UpdateAsync(Turma aluno);
        Task<bool> NomeExistsAsync(string turma);
    }
}
