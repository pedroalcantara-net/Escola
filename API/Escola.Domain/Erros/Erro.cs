namespace Escola.Domain.Erros
{
    public class Erro
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public Erro(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
    }
}
