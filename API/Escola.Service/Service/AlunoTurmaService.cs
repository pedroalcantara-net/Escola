using Escola.Application.Interface;
using Escola.Domain.Interface.Repository;

namespace Escola.Application.Service
{
    public class AlunoTurmaService(IAlunoTurmaRepository alunoTurmaRepository) : IAlunoTurmaService
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;
    }
}
