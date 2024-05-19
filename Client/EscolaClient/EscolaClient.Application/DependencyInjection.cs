using EscolaClient.Application.Interface.Service;
using EscolaClient.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaClient.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAlunoService, AlunoService>();
            services.AddTransient<IAlunoTurmaService, AlunoTurmaService>();
            services.AddTransient<ITurmaService, TurmaService>();

            return services;
        }
    }
}
