using Escola.Application.Interface;
using Escola.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Escola.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddTransient<IAlunoService, AlunoService>();
            services.AddTransient<IAlunoTurmaService, AlunoTurmaService>();
            services.AddTransient<ITurmaService, TurmaService>();
            return services;
        }
    }
}
