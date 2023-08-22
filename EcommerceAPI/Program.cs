using EcommerceAPI.Config;
using EcommerceAPI.Repository;
using EcommerceAPI.Repository.Interfaces;
using EcommerceAPI.StripeInfra;
using EcommerceAPI.Token;
using GeekShopping.ProductAPI.Context;
using LoginAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSqlServer<SQLServerContext>(builder.Configuration["ConnectionStrings:Database"]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.GetTempPath()));

builder.Services.AddScoped<ITShoesRepository, TShoesRepository>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<EcommerceAPI.Token.TokenService>();

builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<SQLServerContext>()
                .AddDefaultTokenProviders();

IdentityJwtConfig.ConfigIdentityOptions(builder.Services);

IdentityJwtConfig.ConfigJwtAuthentication(builder);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddStripeInfrastructure(builder.Configuration);

builder.Services.AddCors();
//builder.Services.AddDataProtection();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
