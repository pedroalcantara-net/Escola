using EscolaClient.Application.Contract;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Interface.ApiService
{
    public interface ITurmaApiService
    {
        Task<BaseResponse> DeleteById(int id);
        Task<BaseResponse<IEnumerable<Turma>>> Get();
        Task<BaseResponse<IEnumerable<Aluno>>> GetAluno(int turmaId);
        Task<BaseResponse<Turma>> GetById(int id);
        Task<BaseResponse<Turma>> Post(Turma turma);
        Task<BaseResponse<Turma>> Put(Turma turma);
    }
}
