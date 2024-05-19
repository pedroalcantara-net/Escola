using EscolaClient.Application.Contract;
using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Application.Interface.Service;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Service
{
    public class TurmaService(ITurmaApiService turmaApiService) : ITurmaService
    {
        private readonly ITurmaApiService _turmaApiService = turmaApiService;

        public async Task<BaseResponse<IEnumerable<Turma>>> Get() => await _turmaApiService.Get();
        public async Task<BaseResponse<Turma>> GetById(int id) => await _turmaApiService.GetById(id);
        public async Task<BaseResponse<Turma>> Add(Turma Turma) => await _turmaApiService.Post(Turma);
        public async Task<BaseResponse<Turma>> Update(Turma Turma) => await _turmaApiService.Put(Turma);
        public async Task<BaseResponse> Delete(int id) => await _turmaApiService.DeleteById(id);
    }
}
