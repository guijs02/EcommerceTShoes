using EcommerceAPI.Services.Interfaces;
using EcommerceWeb.Dto;
using EcommerceWeb.Model;
using EcommerceWeb.Services.Serialize;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;

namespace EcommerceWeb.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly HttpClient _http;
        private readonly IProdutoService _produtoService;
        private const string API = "https://localhost:7211/api/Carrinho";
        private const string API_Produto = "https://localhost:7055/api/Produto";
        private const string ERROR_API = "Erro ao realizar a requisição API";
        public CarrinhoService(HttpClient http, IProdutoService produtoService)
        {
            _http = http;
            _produtoService = produtoService;
        }
        public async Task<CarrinhoDeCompraViewModel> AddCart(ProdutoViewModel produto, string UserId)
        {
            var response = await _http.PostAsJsonAsync(API, produto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await SerializadorDeObjetos.Serializador<CarrinhoDeCompraViewModel>(response);
        }
        public async Task<List<CarrinhoDeCompraViewModel>> GetAllCarrinho()
        {
            var response = await _http.GetAsync(API);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<List<CarrinhoDeCompraViewModel>>(response);
        }
        public async Task<CarrinhoDeCompraViewModel> EditCarrinho(ProdutoViewModel produto)
        {
            var response = await _http.PutAsJsonAsync(API, produto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<CarrinhoDeCompraViewModel>(response);
        }
        public async Task<bool> EditCarrinhoQuantidade(CarrinhoDeCompraViewModel carrinho)
        {
            var response = await _http.PutAsJsonAsync($"{API}/editProductDetails", carrinho);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }
        public async Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinho(int id)
        {
            var produto = await _produtoService.GetProduto(id);

            var response = await _http.GetAsync($"{API}/{produto?.Id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<ProdutoCarrinhoDto>(response);
        }
        public async Task<bool> DeleteItemCarrinho(int id)
        {
            var response = await _http.DeleteAsync($"{API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }

        public async Task<bool> Checkout(OrderDetails orderDetails)
        {

            var response = await _http.PostAsJsonAsync($"{API}/checkout", orderDetails);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }
    }
}
