using Escola.Domain.Error;

namespace Escola.API.Contract
{
    public class ErroListResponse
    {
        public IEnumerable<Erro> Erros { get; }

        public ErroListResponse(params Erro[] erros) => Erros = erros;
        public ErroListResponse(IEnumerable<Erro> erros) => Erros = erros;
    }
}
