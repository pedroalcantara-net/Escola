namespace Escola.Application.Contract.Aluno
{
    public class AlunoResponse
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }

        public AlunoResponse()
        {

        }

        public AlunoResponse(Domain.Entity.Aluno aluno)
        {
            Id = aluno.Id;
            Nome = aluno.Nome;
            Usuario = aluno.Usuario;
        }
    }
}
