using Escola.Domain.Interface.Repository;
using Escola.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Escola.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IAlunoRepository, AlunoRepository>();
            services.AddTransient<IAlunoTurmaRepository, AlunoTurmaRepository>();
            services.AddTransient<ITurmaRepository, TurmaRepository>();

            return services;
        }
    }
}
