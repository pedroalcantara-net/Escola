namespace Escola.Application.Contract.Turma
{
    public class TurmaRequest
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
    }
}
