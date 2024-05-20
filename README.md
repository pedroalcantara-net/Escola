
# Escola

Aplicação de gerenciamento de alunos e turmas em uma escola

O projeto foi desenvolvido como parte de um teste, tendo foco na implementação de uma API em .NET, demonstrando competências como .NET 8, SQL Server, Dapper, Razor Pages.


## Instalação

Instale o [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

Clone o projeto
```bash
  git clone https://github.com/pedroalcantara-net/Escola.git
```

Execute o arquivo Escola_Database.sql encontrado na pasta Script em uma base de dados Microsoft SQL Server

Adicione a String de Conexão de sua base de dados ao arquivo appsettings.json , encontrado no projeto Escola.Api
## Rodando localmente


Inicie o servidor

```bash
  dotnet run --project API/Escola.API/Escola.Api.csproj --launch-profile https
```

Inicie o cliente
```bash
  dotnet run --project Client/EscolaClient.Web/EscolaClient.Web.csproj --launch-profile https
```


## Rodando os testes

Para rodar os testes da API, execute o seguinte comando

```bash
  dotnet test API/Escola.Test/Escola.Test.csproj
```

Para rodar os testes do Client, rode o projeto localmente e execute o seguinte comando

```bash
  dotnet test Client/EscolaClient.Test
```

