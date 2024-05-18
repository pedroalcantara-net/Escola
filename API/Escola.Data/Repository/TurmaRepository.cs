using Dapper;
using Escola.Domain.Entity;
using Escola.Domain.Interface.Repository;
using Escola.Persistence.Infrastructure;
using Microsoft.Data.SqlClient;

namespace Escola.Persistence.Repository
{
    public class TurmaRepository(ConnectionString connectionString) : ITurmaRepository
    {
        private readonly ConnectionString _connectionString = connectionString;
        public async Task<Turma> AddAsync(Turma turma)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync(@"INSERT INTO [Turma] ([Curso_Id], [Turma], [Ano]) VALUES(@CURSO_ID, @TURMA, @ANO)", new
            {
                @CURSO_ID = turma.CursoId,
                @TURMA = turma.Nome,
                @ANO = turma.Ano
            });

            return await connection.QuerySingleOrDefaultAsync<Turma>("SELECT TOP (1) *, [Turma] as Nome FROM [Turma] WHERE [Turma] = @TURMA", new
            {
                @TURMA = turma.Nome
            }) ?? turma;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync("DELETE [Turma] WHERE [Id] = @ID", new
            {
                @ID = id
            });
        }

        public async Task<IEnumerable<Turma>> GetAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return await connection.QueryAsync<Turma>("SELECT *, [Turma] as Nome FROM [Turma]");
        }

        public async Task<Turma?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return await connection.QuerySingleOrDefaultAsync<Turma>("SELECT TOP (1) *, [Turma] as Nome FROM [Turma] WHERE [Id] = @ID", new
            {
                @ID = id
            });
        }

        public async Task<Turma> UpdateAsync(Turma turma)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync(@"UPDATE [dbo].[Turma] SET [Curso_Id] = @CURSO_ID,[Turma] = @TURMA,[Ano] = @ANO WHERE [Id] = @ID", new
            {
                @CURSO_ID = turma.CursoId,
                @TURMA = turma.Nome,
                @ANO = turma.Ano
            });

            return turma;
        }

        public async Task<bool> NomeExistsAsync(string nome)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            var turma = await connection.QuerySingleOrDefaultAsync<Turma>("SELECT TOP (1) * FROM [Turma] WHERE [Turma] = @TURMA", new
            {
                @TURMA = nome
            });

            return turma is not null;
        }

        public async Task<IEnumerable<Turma>> GetByAlunoIdAsync(int alunoId)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return await connection.QueryAsync<Turma>("SELECT T.*, [Turma] as Nome FROM [Escola].[dbo].[Aluno_Turma] AT INNER JOIN [Turma] T ON T.Id = AT.Turma_Id WHERE AT.[Aluno_Id] = @ALUNO_ID", new
            {
                @ALUNO_ID = alunoId
            });
        }

    }
}
