﻿using EcommerceWeb.Services.Handle;
using System.Text.Json;

namespace EcommerceWeb.Services.Serialize
{
    public class SerializadorDeObjetos
    {
        const string APPLICATION_JSON = "application/json";
        private static JsonSerializerOptions Options => new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
        };
        public static async Task<T> Serializador<T>(HttpResponseMessage response)
        {
            var contentType = response.Content.Headers.ContentType?.MediaType;

            if (string.IsNullOrEmpty(contentType) || !contentType.Contains(APPLICATION_JSON))
                throw new ApiException("O conteudo em string está nulo ou está retornando algo inadequado para a serialização");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, Options);

        }
    }
}
