using EcommerceAPI.Config;
using EcommerceAPI.Repository;
using EcommerceAPI.Repository.Interfaces;
using EcommerceAPI.StripeInfra;
using EcommerceAPI.Token;
using GeekShopping.ProductAPI.Context;
using LoginAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSqlServer<SQLServerContext>(builder.Configuration["ConnectionStrings:Database"]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ITShoesRepository, TShoesRepository>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<SQLServerContext>();

IdentityJwtConfig.ConfigIdentityOptions(builder.Services);

IdentityJwtConfig.ConfigJwtAuthentication(builder.Services);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddStripeInfrastructure(builder.Configuration);

builder.Services.AddCors();

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

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
