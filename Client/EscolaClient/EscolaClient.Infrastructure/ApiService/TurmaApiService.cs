using EscolaClient.Application.Contract;
using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Domain.Model;
using System.Net.Http.Json;

namespace EscolaClient.Infrastructure.ApiService
{
    public class TurmaApiService(HttpClient httpClient) : ITurmaApiService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<BaseResponse<IEnumerable<Turma>>> Get()
        {
            var response = new BaseResponse<IEnumerable<Turma>>();
            var apiResponse = await _httpClient.GetAsync("turma");

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<IEnumerable<Turma>>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<IEnumerable<Turma>>>();
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Aluno>>> GetAluno(int turmaId)
        {
            var response = new BaseResponse<IEnumerable<Aluno>>();
            var apiResponse = await _httpClient.GetAsync($"turma/{turmaId}/aluno");

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<IEnumerable<Aluno>>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<IEnumerable<Aluno>>>();
            }

            return response;
        }

        public async Task<BaseResponse<Turma>> GetById(int id)
        {
            var response = new BaseResponse<Turma>();
            var apiResponse = await _httpClient.GetAsync($"turma/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<Turma>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<Turma>>();
            }

            return response;
        }

        public async Task<BaseResponse<Turma>> Post(Turma turma)
        {
            var response = new BaseResponse<Turma>();
            var apiResponse = await _httpClient.PostAsJsonAsync($"turma", turma);

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<Turma>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<Turma>>();
            }

            return response;
        }

        public async Task<BaseResponse<Turma>> Put(Turma turma)
        {
            var response = new BaseResponse<Turma>();
            var apiResponse = await _httpClient.PutAsJsonAsync($"turma", turma);

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<Turma>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<Turma>>();
            }

            return response;
        }

        public async Task<BaseResponse> DeleteById(int id)
        {
            var response = new BaseResponse();
            var apiResponse = await _httpClient.DeleteAsync($"turma/{id}");

            if (!apiResponse.IsSuccessStatusCode) response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse>();

            return response;
        }
    }
}
