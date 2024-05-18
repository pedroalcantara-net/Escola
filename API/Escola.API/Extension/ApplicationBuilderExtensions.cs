using Escola.API.Middleware;

namespace Escola.Api.Extension
{
    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(options => options.SwaggerEndpoint("../swagger/v1/swagger.json", "Escola API"));

            return builder;
        }

        internal static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
