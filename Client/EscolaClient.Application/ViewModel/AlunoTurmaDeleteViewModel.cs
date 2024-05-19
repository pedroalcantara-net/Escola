using EscolaClient.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
