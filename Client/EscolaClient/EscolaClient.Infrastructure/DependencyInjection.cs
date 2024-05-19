using EscolaClient.Application.Interface.ApiService;
using EscolaClient.Infrastructure.ApiService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaClient.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var apiUrl = configuration["Url"];

            services.AddTransient<IAlunoApiService, AlunoApiService>();
            services.AddTransient<IAlunoTurmaApiService, AlunoTurmaApiService>();
            services.AddTransient<ITurmaApiService, TurmaApiService>();

            services.AddHttpClient<IAlunoApiService, AlunoApiService>(httpClient => httpClient.BaseAddress = new(apiUrl!));
            services.AddHttpClient<IAlunoTurmaApiService, AlunoTurmaApiService>(httpClient => httpClient.BaseAddress = new(apiUrl!));
            services.AddHttpClient<ITurmaApiService, TurmaApiService>(httpClient => httpClient.BaseAddress = new(apiUrl!));

            return services;
        }
    }
}
