using EscolaClient.Domain.Model;

namespace EscolaClient.Application.ViewModel
{
    public class AlunoTurmaDeleteViewModel
    {
        public Aluno? Aluno { get; set; }
        public Turma? Turma { get; set; }
        public int? AlunoId { get; set; }
        public int? TurmaId { get; set; }
    }
}
