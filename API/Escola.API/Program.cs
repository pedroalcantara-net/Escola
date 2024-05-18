using Escola.Api.Extension;
using Escola.API.Middleware;
using Escola.Application;
using Escola.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddTransient<ExceptionHandlingMiddleware>()
    .AddPersistence()
    .AddService();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandlingMiddleware();

app.Run();
