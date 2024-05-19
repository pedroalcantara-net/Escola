using EscolaClient.Application.Contract;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Interface.Service
{
    public interface IAlunoService
    {
        Task<BaseResponse<Aluno>> Add(Aluno aluno);
        Task<BaseResponse> Delete(int id);
        Task<BaseResponse<IEnumerable<Aluno>>> Get();
        Task<BaseResponse<Aluno>> GetById(int id);
        Task<BaseResponse<IEnumerable<Aluno>>> GetByTurmaId(int id);
        Task<BaseResponse<Aluno>> Update(Aluno aluno);
    }
}
