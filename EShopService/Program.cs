using EShop.Application.Service;
using EShop.Domain.Models;
using EShop.Domain.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICreditCardService, CreditCardService>();         //dopisanee cos brakuje

//builder.Services.AddScoped<IProductService, ProductService>();               //tu tezz :(        

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer());       //dodanie kontekstu bazy danych 
//builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEShopSeeder, EShopSeeder>();

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


//dolozenie seeda- inicjalinych danychh do bazy danych:
//var scope = app.Services.CreateScope();
//var seeder = scope.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    EShopSeeder.Seed(context);
}


app.Run();

