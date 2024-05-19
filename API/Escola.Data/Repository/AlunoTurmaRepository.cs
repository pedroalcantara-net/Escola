using Dapper;
using Escola.Domain.Entity;
using Escola.Domain.Interface.Repository;
using Escola.Persistence.Infrastructure;
using Microsoft.Data.SqlClient;

namespace Escola.Persistence.Repository
{
    public class AlunoTurmaRepository(ConnectionString connectionString) : IAlunoTurmaRepository
    {
        private readonly ConnectionString _connectionString = connectionString;

        public async Task<AlunoTurma> AddAsync(AlunoTurma alunoTurma)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync(@"INSERT INTO [Aluno_Turma] ([Aluno_Id],[Turma_Id]) VALUES (@ALUNO_ID ,@TURMA_ID)", new
            {
                @ALUNO_ID = alunoTurma.AlunoId,
                @TURMA_ID = alunoTurma.TurmaId
            });

            return await connection.QueryFirstOrDefaultAsync<AlunoTurma>("SELECT TOP(1) * FROM [Aluno_Turma] WHERE [Aluno_Id] = @ALUNO_ID AND [Turma_Id] = @TURMA_ID", new
            {
                @ALUNO_ID = alunoTurma.AlunoId,
                @TURMA_ID = alunoTurma.TurmaId
            }) ?? alunoTurma;
        }

        public async Task<bool> AlunoTurmaExistsAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            var alunoTurma = await connection.QueryFirstOrDefaultAsync("SELECT TOP(1) * FROM [Aluno_Turma] WHERE [Aluno_Id] = @ALUNO_ID AND [Turma_Id] = @TURMA_ID", new
            {
                @ALUNO_ID = alunoId,
                @TURMA_ID = turmaId
            });

            return alunoTurma is not null;
        }

        public async Task<IEnumerable<AlunoTurma>> GetByAlunoId(int alunoId)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            var alunoTurma = await connection.QueryAsync<AlunoTurma>("SELECT TOP(1) * FROM [Aluno_Turma] WHERE [Aluno_Id] = @ALUNO_ID", new
            {
                @ALUNO_ID = alunoId,
            });

            return alunoTurma;
        }

        public async Task<IEnumerable<AlunoTurma>> GetByTurmaId(int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            var alunoTurma = await connection.QueryAsync<AlunoTurma>("SELECT TOP(1) * FROM [Aluno_Turma] WHERE [Turma_Id] = @TURMA_ID", new
            {
                @TURMA_ID = turmaId,
            });

            return alunoTurma;
        }

        public async Task DeleteAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync("DELETE [Aluno_Turma] WHERE [Aluno_Id] = @ALUNO_ID AND [Turma_Id] = @TURMA_ID", new
            {
                @ALUNO_ID = alunoId,
                @TURMA_ID = turmaId
            });
        }
    }
}
