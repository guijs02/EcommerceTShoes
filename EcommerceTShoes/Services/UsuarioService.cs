using EcommerceWeb.Services.AuthClient;
using EcommerceWeb.Services.Interfaces;
using EcommerceWeb.Services.Serialize;
using EcommerceWeb.Utils;
using LoginAPI.Dto;
using System.Net;
using System.Net.Http.Json;

namespace EcommerceWeb.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        private readonly TokenAuthenticationProvider _tokenProvider;
        private const string ERROR_API = "Erro ao realizar a requisição API";

        public UsuarioService(HttpClient http, TokenAuthenticationProvider tokenProvider)
        {
            _http = http;
            _tokenProvider = tokenProvider;
        }

        public async Task<bool> Cadastro(CreateUsuarioDto dto)
        {
            var response = await _http.PostAsJsonAsync($"{ServicesUrl.Usuario_API}/cadastro", dto);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new Exception(erro);
            }
            return await SerializadorDeObjetos.Serializador<bool>(response);
        }

        public async Task Login(LoginUsuarioDto dto)
        {
            var response = await _http.PostAsJsonAsync($"{ServicesUrl.Usuario_API}/login", dto);

            if (!response.IsSuccessStatusCode)
            {
                string erro = await TratarResponse(response);
                throw new Exception(erro);
            }
            var token = await response.Content.ReadAsStringAsync();
            await _tokenProvider.LoginTokenAction(token);
            await Task.CompletedTask;

        }
        public async Task Logout()
        {
            try
            {
                await _tokenProvider.Logout();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<string> TratarResponse(HttpResponseMessage responseMessage)
        {
            string response;
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new Exception("Erro na requisição");
                case HttpStatusCode.InternalServerError:
                    {
                        response = await responseMessage.Content.ReadAsStringAsync();
                    }
                    break;
                default:
                    {
                        response = ERROR_API;
                        break;
                    }

            }
            return response;
        }
    }
}
