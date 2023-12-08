using EcommerceAPI.Services;
using EcommerceAPI.Services.Interfaces;
using EcommerceWeb;
using EcommerceWeb.Services;
using EcommerceWeb.Services.AuthClient;
using EcommerceWeb.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


#region Services

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<TokenAuthenticationProvider>();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>
(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>()
);
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazorBootstrap();
#endregion
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
