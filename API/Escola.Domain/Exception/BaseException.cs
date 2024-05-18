using Escola.Domain.Error;

namespace Escola.Domain.Exception
{
    public class BaseException : System.Exception
    {
        public IEnumerable<Erro> Erros { get; set; }

        protected BaseException(Erro erro) => Erros = [erro];

        protected BaseException(params Erro[] erros) => Erros = erros;

        protected BaseException(IEnumerable<Erro> erros) => Erros = erros;
    }
}
