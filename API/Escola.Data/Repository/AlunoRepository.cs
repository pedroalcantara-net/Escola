using Escola.Domain.Entity;
using Escola.Domain.Interface.Repository;

namespace Escola.Persistence.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        public async Task<Aluno> AddAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Aluno>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Aluno> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Aluno> UpdateAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UsuarioExistsAsync(string usuario)
        {
            throw new NotImplementedException();
        }
    }
}
