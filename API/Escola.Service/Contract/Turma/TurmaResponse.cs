namespace Escola.Application.Contract.Turma
{
    public class TurmaResponse
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }

        public TurmaResponse()
        {

        }

        public TurmaResponse(Domain.Entity.Turma turma)
        {
            Id = turma.Id;
            CursoId = turma.CursoId;
            Nome = turma.Nome;
            Ano = turma.Ano;
        }
    }
}
