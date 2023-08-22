namespace EcommerceTShoes.Services.Interfaces
{
    public interface IPagamentoService
    {
        Task<HttpResponseMessage> ObterPagePagamento();
    }
}
