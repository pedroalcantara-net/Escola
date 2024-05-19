using EscolaClient.Application.Contract;
using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Application.Interface.Service;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Service
{
    public class AlunoService(IAlunoApiService alunoApiService) : IAlunoService
    {
        private readonly IAlunoApiService _alunoApiService = alunoApiService;

        public async Task<BaseResponse<IEnumerable<Aluno>>> Get() => await _alunoApiService.Get();
        public async Task<BaseResponse<Aluno>> GetById(int id) => await _alunoApiService.GetById(id);
        public async Task<BaseResponse<IEnumerable<Aluno>>> GetByTurmaId(int id) => await _alunoApiService.GetByTurmaId(id);
        public async Task<BaseResponse<Aluno>> Add(Aluno aluno) => await _alunoApiService.Post(aluno);
        public async Task<BaseResponse<Aluno>> Update(Aluno aluno) => await _alunoApiService.Put(aluno);
        public async Task<BaseResponse> Delete(int id) => await _alunoApiService.DeleteById(id);
    }
}
