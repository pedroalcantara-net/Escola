namespace EscolaClient.Domain.Model
{
    public class Aluno
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }
        public string? SenhaConfirmacao { get; set; }
    }
}
