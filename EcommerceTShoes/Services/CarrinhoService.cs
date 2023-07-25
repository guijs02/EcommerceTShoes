using EcommerceAPI.Services.Interfaces;
using EcommerceTShoes.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace EcommerceAPI.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly HttpClient _http;
        private const string API = "https://localhost:7064/api/Carrinho";
        private readonly JsonSerializerOptions options;
        private const string ERROR_API = "Erro ao realizar a requisição API";
        public CarrinhoService(HttpClient http)
        {
            _http = http;
            options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
            };
        }
        public async Task<CarrinhoDeCompra> AddCart(Produto produto)
        {
            var response = await _http.PostAsJsonAsync(API, produto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await Serializador<CarrinhoDeCompra>(response);
        }
        public async Task<List<CarrinhoDeCompra>> GetAllCarrinho()
        {
            var response = await _http.GetAsync(API);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await Serializador<List<CarrinhoDeCompra>>(response);
        }   
        public async Task<CarrinhoDeCompra> EditCarrinho(Produto produto)
        {
            var response = await _http.PutAsJsonAsync(API, produto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await Serializador<CarrinhoDeCompra>(response);
        }  
        public async Task<Produto> GetByIdProdutoCarrinho(int id)
        {
            var response = await _http.GetAsync($"{API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await Serializador<Produto>(response);
        }
        public async Task<bool> DeleteItemCarrinho(int id)
        {
            var response = await _http.DeleteAsync($"{API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await Serializador<bool>(response);
        }
        public async Task<T> Serializador<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            if(string.IsNullOrEmpty(json))
                throw new Exception("O conteudo em string está nulo");

            return JsonSerializer.Deserialize<T>(json, options);

        }
    

    }
}
