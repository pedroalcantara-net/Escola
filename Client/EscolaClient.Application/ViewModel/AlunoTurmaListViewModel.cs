using EscolaClient.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaClient.Application.ViewModel
{
    public class AlunoTurmaListViewModel
    {
        public IEnumerable<Aluno>? Alunos { get; set; }
        public Turma? Turma { get; set; }
    }
}
