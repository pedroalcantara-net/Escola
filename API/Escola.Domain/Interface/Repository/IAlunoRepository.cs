using Escola.Domain.Entity;

namespace Escola.Domain.Interface.Repository
{
    public interface IAlunoRepository
    {
        Task<Aluno> AddAsync(Aluno aluno);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<Aluno>> GetAsync();
        Task<Aluno?> GetByIdAsync(int id);
        Task<Aluno> UpdateAsync(Aluno aluno);
        Task<bool> UsuarioExistsAsync(string usuario);
        Task<IEnumerable<Aluno>> GetByTurmaIdAsync(int turmaId);
    }
}
