namespace EscolaClient.Application.Contract
{
    public class BaseResponse
    {
        public IEnumerable<ErroResponse>? Erros { get; set; }
    }

    public class BaseResponse<T> : BaseResponse where T : class
    {
        public T? Response { get; set; }
    }
}
