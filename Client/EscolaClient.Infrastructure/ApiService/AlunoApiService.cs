using EscolaClient.Application.Contract;
using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Domain.Model;
using System.Net.Http.Json;

namespace EscolaClient.Infrastructure.ApiService
{
    public class AlunoApiService(HttpClient httpClient) : IAlunoApiService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<BaseResponse<IEnumerable<Aluno>>> Get()
        {
            var response = new BaseResponse<IEnumerable<Aluno>>();
            var apiResponse = await _httpClient.GetAsync("aluno");

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

        public async Task<BaseResponse<IEnumerable<Aluno>>> GetByTurmaId(int id)
        {
            var response = new BaseResponse<IEnumerable<Aluno>>();
            var apiResponse = await _httpClient.GetAsync($"turma/{id}/aluno");

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

        public async Task<BaseResponse<IEnumerable<Turma>>> GetTurma(int alunoId)
        {
            var response = new BaseResponse<IEnumerable<Turma>>();
            var apiResponse = await _httpClient.GetAsync($"aluno/{alunoId}/turma");

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

        public async Task<BaseResponse<Aluno>> GetById(int id)
        {
            var response = new BaseResponse<Aluno>();
            var apiResponse = await _httpClient.GetAsync($"aluno/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<Aluno>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<Aluno>>();
            }

            return response;
        }

        public async Task<BaseResponse<Aluno>> Post(Aluno aluno)
        {
            var response = new BaseResponse<Aluno>();
            var apiResponse = await _httpClient.PostAsJsonAsync($"aluno", aluno);

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<Aluno>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<Aluno>>();
            }

            return response;
        }

        public async Task<BaseResponse<Aluno>> Put(Aluno aluno)
        {
            var response = new BaseResponse<Aluno>();
            var apiResponse = await _httpClient.PutAsJsonAsync($"aluno", aluno);

            if (apiResponse.IsSuccessStatusCode)
            {
                response.Response = await apiResponse.Content.ReadFromJsonAsync<Aluno>();
            }
            else
            {
                response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse<Aluno>>();
            }

            return response;
        }

        public async Task<BaseResponse> DeleteById(int id)
        {
            var response = new BaseResponse();
            var apiResponse = await _httpClient.DeleteAsync($"aluno/{id}");

            if (!apiResponse.IsSuccessStatusCode) response = await apiResponse.Content.ReadFromJsonAsync<BaseResponse>();

            return response;
        }
    }
}
