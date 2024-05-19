using EscolaClient.Application.Contract;
using EscolaClient.Application.ViewModel;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Interface.Service
{
    public interface IAlunoTurmaService
    {
        Task<BaseResponse<AlunoTurma>> Add(AlunoTurmaCreateViewModel model);
        Task<BaseResponse> Delete(AlunoTurmaDeleteViewModel model);
    }
}