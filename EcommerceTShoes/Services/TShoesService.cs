using EcommerceAPI.Services.Interfaces;
using EcommerceTShoes.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace EcommerceAPI.Services
{
    public class TShoesService : ITShoesService
    {
        private readonly HttpClient _http;
        private const string API = "https://localhost:7064/api/TShoes";
        private readonly JsonSerializerOptions options;
        private const string ERROR_API = "Erro ao realizar a requisição API";
        public TShoesService(HttpClient http)
        {
            _http = http;
            options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
            };
        }
        public async Task<List<Produto>> GetAllProdutos()
        {
            var response = await _http.GetAsync(API);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await Serializador<List<Produto>>(response);

        }    
        public async Task<List<Produto>> GetProdutosByGenero(int idgenero)
        {
            var response = await _http.GetAsync($"{API}/GetByGenero/{idgenero}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await Serializador<List<Produto>>(response);

        }     
        public async Task<Produto> GetProduto(int id)
        {
            var response = await _http.GetAsync($"{API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await Serializador<Produto>(response);

        }
        public async Task<T> Serializador<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
                throw new Exception("O conteudo em string está nulo");

            return JsonSerializer.Deserialize<T>(json, options);

        }
    }
}
