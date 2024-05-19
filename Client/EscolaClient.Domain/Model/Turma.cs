namespace EscolaClient.Domain.Model
{
    public class Turma
    {
        public int? Id { get; set; }
        public int? CursoId { get; set; }
        public string? Nome { get; set; }
        public int? Ano { get; set; }
    }
}
