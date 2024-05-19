using EscolaClient.Application.Contract;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Interface.Service
{
    public interface ITurmaService
    {
        Task<BaseResponse<Turma>> Add(Turma Turma);
        Task<BaseResponse> Delete(int id);
        Task<BaseResponse<IEnumerable<Turma>>> Get();
        Task<BaseResponse<Turma>> GetById(int id);
        Task<BaseResponse<Turma>> Update(Turma Turma);
    }
}