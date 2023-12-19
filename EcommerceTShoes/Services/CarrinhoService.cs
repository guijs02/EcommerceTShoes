using EcommerceAPI.Services.Interfaces;
using EcommerceWeb.Dto;
using EcommerceWeb.Model;
using EcommerceWeb.Services.Handle;
using EcommerceWeb.Services.Serialize;
using EcommerceWeb.Utils;
using System.Net.Http.Json;

namespace EcommerceWeb.Services
{
    public class CarrinhoService : BaseService, ICarrinhoService
    {
        private readonly HttpClient _http;
        private readonly IProdutoService _produtoService;
        public CarrinhoService(HttpClient http, IProdutoService produtoService)
        {
            _http = http;
            _produtoService = produtoService;
        }
        public async Task<CarrinhoDeCompraViewModel> AddCart(ProdutoViewModel produto)
        {
            var url = BuildUrl(ServicesUrl.Cart_API);

            var response = await _http.PostAsJsonAsync(url, produto);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }
            return await SerializadorDeObjetos.Serializador<CarrinhoDeCompraViewModel>(response);
        }
        public async Task<List<CarrinhoDeCompraViewModel>> GetAllCarrinho()
        {
            var url = BuildUrl(ServicesUrl.Cart_API);

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }

            return await SerializadorDeObjetos.Serializador<List<CarrinhoDeCompraViewModel>>(response);
        }
        public async Task<CarrinhoDeCompraViewModel> EditCarrinho(ProdutoViewModel produto)
        {
            var url = BuildUrl(ServicesUrl.Cart_API);

            var response = await _http.PutAsJsonAsync(url, produto);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }

            return await SerializadorDeObjetos.Serializador<CarrinhoDeCompraViewModel>(response);
        }
        public async Task<bool> EditCarrinhoQuantidade(CarrinhoDeCompraViewModel carrinho)
        {
            var url = BuildUrl(ServicesUrl.Cart_API, "/editProductDetails");

            var response = await _http.PutAsJsonAsync(url, carrinho);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }
        public async Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinho(int id)
        {
            var produto = await _produtoService.GetProduto(id);

            var url = BuildUrl(ServicesUrl.Cart_API, $"/{produto?.Id}");

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }

            return await SerializadorDeObjetos.Serializador<ProdutoCarrinhoDto>(response);
        }
        public async Task<bool> DeleteItemCarrinho(int id)
        {
            var url = BuildUrl(ServicesUrl.Cart_API, $"/{id}");

            var response = await _http.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }

        public async Task<bool> Checkout(OrderDetails orderDetails)
        {
            var url = BuildUrl(ServicesUrl.Cart_API, "/checkout");

            var response = await _http.PostAsJsonAsync(url, orderDetails);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }

            return await SerializadorDeObjetos.Serializador<bool>(response);
        }
    }
}
