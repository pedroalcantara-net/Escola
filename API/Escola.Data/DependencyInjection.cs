using Dapper;
using Escola.Domain.Interface.Repository;
using Escola.Persistence.Infrastructure;
using Escola.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Escola.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey);

            services.AddSingleton(new ConnectionString(connectionString!));

            services.AddTransient<IAlunoRepository, AlunoRepository>();
            services.AddTransient<IAlunoTurmaRepository, AlunoTurmaRepository>();
            services.AddTransient<ITurmaRepository, TurmaRepository>();

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }
    }
}
