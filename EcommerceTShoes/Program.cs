using EcommerceAPI.Services.Interfaces;
using EcommerceAPI.Services;
using EcommerceWeb;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EcommerceWeb.Services;
using EcommerceWeb.Services.Interfaces;
using EcommerceWeb.Auth;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Services
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();

builder.Services.AddScoped<TokenAuthenticationProvider>();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>
(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>()
);
builder.Services.AddAuthorizationCore();

#endregion
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
