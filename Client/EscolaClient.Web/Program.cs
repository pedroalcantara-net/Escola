using EscolaClient.Application;
using EscolaClient.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddHttpClient()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Aluno}/{action=Index}/{id?}");

app.Run();
