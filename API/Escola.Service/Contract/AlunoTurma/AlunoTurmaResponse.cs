using Escola.Application.Contract.Turma;

namespace Escola.Application.Contract.AlunoTurma
{
    public class AlunoTurmaResponse
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public AlunoTurmaResponse Aluno { get; set; }
        public TurmaResponse Turma { get; set; }

        public AlunoTurmaResponse()
        {

        }

        public AlunoTurmaResponse(Domain.Entity.AlunoTurma alunoTurma)
        {
            AlunoId = alunoTurma.AlunoId;
            TurmaId = alunoTurma.TurmaId;
        }
    }
}
