using EscolaClient.Application.Contract;
using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Domain.Model;
using System.Net.Http.Json;

namespace EscolaClient.Infrastructure.ApiService
{
    public class AlunoTurmaApiService(HttpClient httpClient) : IAlunoTurmaApiService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<BaseResponse> Delete(AlunoTurma alunoTurma)
        {
            var response = new BaseResponse();
            var apiResponse = await _httpClient.DeleteAsync($"alunoturma/turma/{alunoTurma.TurmaId}/aluno/{alunoTurma.AlunoId}");

            if (!apiResponse.IsSuccessStatusCode)
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse>();
            }

            return response;
        }

        public async Task<BaseResponse<AlunoTurma>> Post(AlunoTurma alunoTurma)
        {
            var response = new BaseResponse<AlunoTurma>();
            var apiResponse = await _httpClient.PostAsJsonAsync($"alunoturma", alunoTurma);

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<AlunoTurma>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<AlunoTurma>>();
            }

            return response;
        }
    }
}
