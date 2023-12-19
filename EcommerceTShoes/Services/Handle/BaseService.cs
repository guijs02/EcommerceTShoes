using EcommerceWeb.Utils;
using System.Net;

namespace EcommerceWeb.Services.Handle
{
    public class BaseService
    {
        private const string ERROR_API = "Erro ao realizar a requisição API";
        public async Task<string> TratarResponse(HttpResponseMessage responseMessage)
        {
            string response = responseMessage.StatusCode switch
            {
                HttpStatusCode.BadRequest => $"Requisição inválida: {responseMessage.ReasonPhrase}",
                HttpStatusCode.InternalServerError => await responseMessage.Content.ReadAsStringAsync(),
                _ => $"{ERROR_API}: {responseMessage.ReasonPhrase}",
            };
            return response;
        }

        public string BuildUrl(string uri, string path = "")
        {
            if(string.IsNullOrEmpty(path))
                return uri;

            
            var uriCompleted = new UriBuilder(ServicesUrl.Usuario_API)
            {
                Path = $"{uri}"+path
            };

           return uriCompleted.Path;
           
        }
    }
}
