using EcommerceWeb.Services.Serialize;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;

namespace EcommerceWeb.Auth
{
    public class TokenAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        private readonly string tokenKey = "tokenKey";

        private AuthenticationState notAuthenticate => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public TokenAuthenticationProvider(IJSRuntime js, HttpClient http, IConfiguration configuration)
        {
            _js = js;
            _http = http;
            //tokenKey = configuration.GetSection("TShoesSettings:SecretKey").Value;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", tokenKey);

            if (string.IsNullOrEmpty(token))
            {
                return notAuthenticate;
            }
            return CreateAuthenticationState(token);

        }
         
        public async Task LoginTokenAction(string token)
        {
            await _js.InvokeAsync<object>("localStorage.setItem", tokenKey, token);
            var authState = CreateAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
        public async Task Logout()
        {
            await _js.InvokeAsync<object>("localStorage.removeItem", tokenKey);
            _http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(notAuthenticate));
        }

        public AuthenticationState CreateAuthenticationState(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var userClaims = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtExtension.ParseClaimsFromJwt(token), "jwt")));
            return userClaims;
        }
        
    }
}
