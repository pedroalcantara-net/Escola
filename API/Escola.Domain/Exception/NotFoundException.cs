using Escola.Domain.Erros;

namespace Escola.Domain.Exception
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(Erro erro) : base(erro) { }

        public NotFoundException(params Erro[] erros) : base(erros) { }

        public NotFoundException(IEnumerable<Erro> erros) : base(erros) { }
    }
}
