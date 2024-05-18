
using Escola.API.Contract;
using Escola.Domain.Erros;
using Escola.Domain.Exception;
using System.Net;
using System.Text.Json;

namespace Escola.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            (var statusCode, var erros) = GetStatusCode(ex);

            httpContext.Response.ContentType = ContentTypes.Json;
            httpContext.Response.StatusCode = (int)statusCode;

            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var erroResponse = JsonSerializer.Serialize(new ErroResponse(erros), serializerOptions);

            await httpContext.Response.WriteAsync(erroResponse);
        }

        public (HttpStatusCode, IEnumerable<Erro>) GetStatusCode(Exception ex)
            => ex switch
            {
                NotFoundException notFoundException => (HttpStatusCode.NotFound, notFoundException.Erros),
                BadRequestException badRequestException => (HttpStatusCode.BadRequest, badRequestException.Erros),
                _ => (HttpStatusCode.InternalServerError, [new Erro("Server.Unexpected", "O servidor encontrou um erro inesperado.")])
            };
    }
}
