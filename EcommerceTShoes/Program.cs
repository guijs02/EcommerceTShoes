using EcommerceAPI.Services.Interfaces;
using EcommerceAPI.Services;
using EcommerceTShoes;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EcommerceTShoes.Pages.Login;
using EcommerceTShoes.Services;
using EcommerceTShoes.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Services
builder.Services.AddScoped<ITShoesService, TShoesService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

#endregion
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
