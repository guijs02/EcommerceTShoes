namespace EcommerceWeb.Services.Interfaces
{
    public interface IPagamentoService
    {
        Task<HttpResponseMessage> ObterPagePagamento();
    }
}
