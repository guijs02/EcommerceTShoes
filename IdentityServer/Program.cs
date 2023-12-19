using AutoMapper;
using IdentityServer.Context;
using IdentityServer.Models;
using IdentityServerAPI.Application.Config;
using IdentityServerAPI.Application.Token;
using IdentityServerAPI.Infraestructure.Repository;
using IdentityServerAPI.Infraestructure.Repository.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSqlServer<SQLServerContext>(builder.Configuration["ConnectionStrings:Database"]);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.GetTempPath()));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<SQLServerContext>()
                .AddDefaultTokenProviders();

IdentityJwtConfig.ConfigIdentityOptions(builder.Services);

IdentityJwtConfig.ConfigJwtAuthentication(builder);

IdentityJwtConfig.GoogleAuthentication(builder);

builder.Services.AddHttpClient();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.Run();
