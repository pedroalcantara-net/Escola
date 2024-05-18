namespace Escola.API.Contract
{
    public static class ApiRoutes
    {
        public const string Controller = "v1/[controller]";
        public static class Aluno
        {
            public const string Id = "{id:int}";
        }

        public static class Turma
        {
            public const string Id = "{id:int}";
        }
    }
}
