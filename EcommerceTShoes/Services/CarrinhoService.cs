using EcommerceAPI.Services.Interfaces;
using EcommerceWeb.Dto;
using EcommerceWeb.Model;
using EcommerceWeb.Services.Serialize;
using EcommerceWeb.Utils;
using System.Net.Http.Json;

namespace EcommerceWeb.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly HttpClient _http;
        private readonly IProdutoService _produtoService;
        private const string ERROR_API = "Erro ao realizar a requisição API";
        public CarrinhoService(HttpClient http, IProdutoService produtoService)
        {
            _http = http;
            _produtoService = produtoService;
        }
        public async Task<CarrinhoDeCompraViewModel> AddCart(ProdutoViewModel produto)
        {
            var response = await _http.PostAsJsonAsync(ServicesUrl.Cart_API, produto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            return await SerializadorDeObjetos.Serializador<CarrinhoDeCompraViewModel>(response);
        }
        public async Task<List<CarrinhoDeCompraViewModel>> GetAllCarrinho()
        {
            var response = await _http.GetAsync(ServicesUrl.Cart_API);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<List<CarrinhoDeCompraViewModel>>(response);
        }
        public async Task<CarrinhoDeCompraViewModel> EditCarrinho(ProdutoViewModel produto)
        {
            var response = await _http.PutAsJsonAsync(ServicesUrl.Cart_API, produto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<CarrinhoDeCompraViewModel>(response);
        }
        public async Task<bool> EditCarrinhoQuantidade(CarrinhoDeCompraViewModel carrinho)
        {
            var response = await _http.PutAsJsonAsync($"{ServicesUrl.Cart_API}/editProductDetails", carrinho);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }
        public async Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinho(int id)
        {
            var produto = await _produtoService.GetProduto(id);

            var response = await _http.GetAsync($"{ServicesUrl.Cart_API}/{produto?.Id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<ProdutoCarrinhoDto>(response);
        }
        public async Task<bool> DeleteItemCarrinho(int id)
        {
            var response = await _http.DeleteAsync($"{ServicesUrl.Cart_API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }

        public async Task<bool> Checkout(OrderDetails orderDetails)
        {

            var response = await _http.PostAsJsonAsync($"{ServicesUrl.Cart_API}/checkout", orderDetails);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }
    }
}
