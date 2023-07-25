using EcommerceTShoes.Model;
using EcommerceTShoes.Services.Interfaces;
using LoginAPI.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace EcommerceTShoes.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        private const string API = "https://localhost:7064/api/Usuario";
        private const string ERROR_API = "Erro ao realizar a requisição API";

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task Cadastro(CreateUsuarioDto dto)
        {
            var response = await _http.PostAsJsonAsync($"{API}/cadastro",dto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            await Task.CompletedTask;
        }

        public async Task Login(LoginUsuarioDto dto)
        {
            var response = await _http.PostAsJsonAsync($"{API}/login", dto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(ERROR_API);
            }
            await Task.CompletedTask;
        }
    }
}
