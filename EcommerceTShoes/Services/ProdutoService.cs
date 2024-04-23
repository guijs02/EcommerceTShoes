using EcommerceAPI.Services.Interfaces;
using EcommerceWeb.Dto;
using EcommerceWeb.Model;
using EcommerceWeb.Services.Handle;
using EcommerceWeb.Services.Serialize;
using EcommerceWeb.Utils;

namespace EcommerceAPI.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        public ProdutoService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }
        public async Task<List<ProdutoDto>> GetAllProdutos()
        {
            var response = await _http.GetAsync(ServicesUrl.Product_API);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }
            return await SerializadorDeObjetos.Serializador<List<ProdutoDto>>(response);

        }
        public async Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero)
        {
            var response = await _http.GetAsync($"{ServicesUrl.Product_API}/GetByGenero/{idgenero}");

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }
            return await SerializadorDeObjetos.Serializador<List<ProdutoDto>>(response);

        }
        public async Task<ProdutoViewModel> GetProduto(int id)
        {
            var response = await _http.GetAsync($"{ServicesUrl.Product_API}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new ApiException(erro);
            }
            return await SerializadorDeObjetos.Serializador<ProdutoViewModel>(response);

        }

    }
}
