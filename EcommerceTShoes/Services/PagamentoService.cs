using EcommerceTShoes.Model;
using EcommerceTShoes.Services.Interfaces;
using EcommerceTShoes.Services.Serialize;
using System.Net.Http.Json;

namespace EcommerceTShoes.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly HttpClient _http;
        private const string API = "https://localhost:7064/api/Pagamento";
        private const string ERROR_API = "Ocorreu um erro não esperado";
        public PagamentoService()
        {
            _http = new HttpClient();
        }
        public async Task<HttpResponseMessage> ObterPagePagamento()
        {
            try
            {

                var response = await _http.GetAsync(API);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(ERROR_API);
                }
                return response;

            }
            catch (Exception ex)
            {

                throw;

            }
        }
    }
}
