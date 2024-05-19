using EscolaClient.Application.Contract;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Interface.ApiService
{
    public interface IAlunoApiService
    {
        Task<BaseResponse> DeleteById(int id);
        Task<BaseResponse<IEnumerable<Aluno>>> Get();
        Task<BaseResponse<Aluno>> GetById(int id);
        Task<BaseResponse<IEnumerable<Turma>>> GetTurma(int alunoId);
        Task<BaseResponse<Aluno>> Post(Aluno aluno);
        Task<BaseResponse<Aluno>> Put(Aluno aluno);
        Task<BaseResponse<IEnumerable<Aluno>>> GetByTurmaId(int id);
    }
}