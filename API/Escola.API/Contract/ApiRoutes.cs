namespace Escola.API.Contract
{
    public static class ApiRoutes
    {
        public const string Controller = "v1/[controller]";
        public static class Aluno
        {
            public const string Id = "{id:int}";
            public const string Turma = "{id:int}/turma";
        }

        public static class Turma
        {
            public const string Id = "{id:int}";
            public const string Aluno = "{id:int}/aluno";
        }
    }
}
