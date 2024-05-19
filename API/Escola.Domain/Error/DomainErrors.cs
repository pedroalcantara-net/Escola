namespace Escola.Domain.Error
{
    public static class DomainErrors
    {
        public static class Aluno
        {
            public static Erro NotFound => new(
                "Aluno.NotFound",
                "O Aluno com o identificador informado não pôde ser encontrado."
                );

            public static Erro NoneFound => new(
                "Aluno.NoneFound",
                "Nenhum aluno encontrado."
                );

            public static Erro UsuarioExists => new(
                "Aluno.UsuarioExists",
                "Já existe um aluno cadastrado com o usuário informado."
                );

            public static Erro SenhaDoesNotMatch => new(
                "Aluno.SenhaDoesNotMatch",
                "A Senha e a Confirmação devem ser idênticas."
                );

            public static Erro SenhaUnsafe => new(
                "Aluno.SenhaNotSafe",
                "A Senha informada não cumpre os requisitos de segurança."
                );

            public static Erro HasTurma => new(
                "Aluno.HasTurma",
                "O Aluno está matriculado em uma ou mais turmas e não pode ser excluído."
                );
        }

        public static class Turma
        {
            public static Erro NotFound => new(
                "Turma.NotFound",
                "A Turma com o identificador informado não pôde ser encontrada."
                );

            public static Erro NoneFound => new(
                "Turma.NoneFound",
                "Nenhuma turma encontrada."
                );

            public static Erro NomeExists => new(
                "Turma.NomeExists",
                "Uma turma com o nome informado já existe."
                );

            public static Erro InvalidAno => new(
                "Turma.InvalidAno",
                "O ano informado é inferior à data atual."
                );

            public static Erro HasAluno => new(
                "Aluno.HasTurma",
                "A Turma possui um ou mais Alunos matriculados e não pode ser excluída."
                );
        }

        public static class AlunoTurma
        {
            public static Erro NotFound => new(
                "Turma.NotFound",
                "O relacionamento de Aluno e Turma com os identificadores informados não pôde ser encontrado."
                );

            public static Erro AlreadyExists => new(
                "Turma.NotFound",
                "Já existe um relacionamento entre o Aluno e a Turma com os identificadores informados."
                );
        }
    }
}
