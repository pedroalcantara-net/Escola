using EscolaClient.Application.Contract;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Interface.ApiService
{
    public interface IAlunoTurmaApiService
    {
        Task<BaseResponse> Delete(AlunoTurma alunoTurma);
        Task<BaseResponse<AlunoTurma>> Post(AlunoTurma alunoTurma);
    }
}
