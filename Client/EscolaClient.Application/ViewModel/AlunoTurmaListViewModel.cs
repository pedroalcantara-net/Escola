using EscolaClient.Domain.Model;

namespace EscolaClient.Application.ViewModel
{
    public class AlunoTurmaListViewModel
    {
        public IEnumerable<Aluno>? Alunos { get; set; }
        public Turma? Turma { get; set; }
    }
}
