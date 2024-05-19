using EscolaClient.Domain.Model;

namespace EscolaClient.Application.ViewModel
{
    public class AlunoTurmaCreateViewModel
    {
        public IEnumerable<Aluno>? Alunos { get; set; }
        public IEnumerable<Turma>? Turmas { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
