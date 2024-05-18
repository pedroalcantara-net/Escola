using Dapper;
using Escola.Domain.Entity;
using Escola.Domain.Interface.Repository;
using Escola.Persistence.Infrastructure;
using Microsoft.Data.SqlClient;

namespace Escola.Persistence.Repository
{
    public class AlunoRepository(ConnectionString connectionString) : IAlunoRepository
    {
        private readonly ConnectionString _connectionString = connectionString;

        public async Task<Aluno> AddAsync(Aluno aluno)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync(@"INSERT INTO [Aluno] ([Nome], [Usuario], [Senha]) VALUES (@NOME, @USUARIO, @SENHA)", new
            {
                @NOME = aluno.Nome,
                @USUARIO = aluno.Usuario,
                @SENHA = aluno.Senha
            });

            return await connection.QuerySingleOrDefaultAsync<Aluno>("SELECT TOP (1) * FROM [Aluno] WHERE [Usuario] = @USUARIO", new
            {
                @USUARIO = aluno.Usuario
            }) ?? aluno;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync("DELETE [Aluno] WHERE [Id] = @ID", new
            {
                @ID = id
            });
        }

        public async Task<IEnumerable<Aluno>> GetAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return await connection.QueryAsync<Aluno>("SELECT * FROM [Aluno]");
        }

        public async Task<Aluno?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return await connection.QuerySingleOrDefaultAsync<Aluno>("SELECT TOP (1) * FROM [Aluno] WHERE [Id] = @ID", new
            {
                @ID = id
            });
        }

        public async Task<Aluno> UpdateAsync(Aluno aluno)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync(@"UPDATE [Aluno] SET [Nome] = @NOME, [Usuario] = @USUARIO, [Senha] = @SENHA WHERE [Id] = @ID", new
            {
                @NOME = aluno.Nome,
                @USUARIO = aluno.Usuario,
                @SENHA = aluno.Senha,
                @ID = aluno.Id
            });

            return aluno;
        }

        public async Task<bool> UsuarioExistsAsync(string usuario)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            var aluno = await connection.QuerySingleOrDefaultAsync<Aluno>("SELECT TOP (1) * FROM [Aluno] WHERE [Usuario] = @USUARIO", new
            {
                @USUARIO = usuario
            });

            return aluno is not null;
        }

        public async Task<IEnumerable<Aluno>> GetByTurmaIdAsync(int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return await connection.QueryAsync<Aluno>("SELECT A.* FROM [Aluno_Turma] AT INNER JOIN [Aluno] A ON AT.Aluno_Id = A.Id WHERE AT.[Turma_Id] = @TURMA_ID", new
            {
                @TURMA_ID = turmaId
            });
        }
    }
}
