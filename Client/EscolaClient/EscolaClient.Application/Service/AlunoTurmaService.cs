using EscolaClient.Application.Contract;
using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Application.Interface.Service;
using EscolaClient.Application.ViewModel;
using EscolaClient.Domain.Model;

namespace EscolaClient.Application.Service
{
    public class AlunoTurmaService(IAlunoTurmaApiService alunoTurmaApiService) : IAlunoTurmaService
    {
        private readonly IAlunoTurmaApiService _alunoTurmaApiService = alunoTurmaApiService;

        public async Task<BaseResponse<AlunoTurma>> Add(AlunoTurmaCreateViewModel model)
        {
            var alunoTurma = new AlunoTurma()
            {
                AlunoId = model.AlunoId,
                TurmaId = model.TurmaId
            };

            return await _alunoTurmaApiService.Post(alunoTurma);
        }
        public async Task<BaseResponse> Delete(AlunoTurmaDeleteViewModel model)
        {
            var alunoTurma = new AlunoTurma()
            {
                AlunoId = model.AlunoId ?? 0,
                TurmaId = model.TurmaId ?? 0
            };

            return await _alunoTurmaApiService.Delete(alunoTurma);
        }
    }
}
