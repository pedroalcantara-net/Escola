using Escola.Domain.Entity;
using Escola.Domain.Interface.Repository;
using Microsoft.Data.SqlClient;

namespace Escola.Persistence.Repository
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly SqlConnection _connection;
        public AlunoTurma Add(AlunoTurma Turma)
        {
            throw new NotImplementedException();
        }

        public void Delete(int turmaId, int alunoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlunoTurma> GetByAlunoId(int alunoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlunoTurma> GetByTurmaId(int turmaId)
        {
            throw new NotImplementedException();
        }
    }
}
