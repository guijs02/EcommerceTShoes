using EcommerceAPI.Services.Interfaces;
using EcommerceWeb.Dto;
using EcommerceWeb.Model;
using EcommerceWeb.Services.Serialize;
using EcommerceWeb.Utils;

namespace EcommerceAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _http;
        private const string ERROR_API = "Erro ao realizar a requisição API";
        public ProdutoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<ProdutoDto>> GetAllProdutos()
        {
            var response = await _http.GetAsync(ServicesUrl.Product_API);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await SerializadorDeObjetos.Serializador<List<ProdutoDto>>(response);

        }
        public async Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero)
        {
            var response = await _http.GetAsync($"{ServicesUrl.Product_API}/GetByGenero/{idgenero}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await SerializadorDeObjetos.Serializador<List<ProdutoDto>>(response);

        }
        public async Task<ProdutoViewModel> GetProduto(int id)
        {
            var response = await _http.GetAsync($"{ServicesUrl.Product_API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await SerializadorDeObjetos.Serializador<ProdutoViewModel>(response);

        }

    }
}
