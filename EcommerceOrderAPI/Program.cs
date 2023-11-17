using EcommerceOrderAPI.Context;
using EcommerceOrderAPI.RabbitMQConsumer;
using EcommerceOrderAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration["ConnectionStrings:Database"];

builder.Services.AddDbContext<SQLServerContext>(opts => opts.UseSqlServer(connection));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbcontext = new DbContextOptionsBuilder<SQLServerContext>();
dbcontext.UseSqlServer(connection);

builder.Services.AddSingleton(new OrderRepository(dbcontext.Options));

builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
