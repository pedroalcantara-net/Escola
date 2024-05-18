using Escola.Domain.Error;

namespace Escola.Domain.Exception
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(Erro erro) : base(erro) { }

        public BadRequestException(params Erro[] erros) : base(erros) { }

        public BadRequestException(IEnumerable<Erro> erros) : base(erros) { }
    }
}
