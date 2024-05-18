using Escola.Domain.Error;

namespace Escola.API.Contract
{
    public class ErroResponse
    {
        public IEnumerable<Erro> Erros { get; }

        public ErroResponse(params Erro[] erros) => Erros = erros;
        public ErroResponse(IEnumerable<Erro> erros) => Erros = erros;
    }
}
