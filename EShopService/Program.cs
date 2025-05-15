using EShop.Application.Service;
using EShop.Domain.Models;
using EShop.Domain.Seeders;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using EShop.Domain.Extensions;

namespace EShopService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ICreditCardService, CreditCardService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer());       //dodanie kontekstu bazy danych 

            builder.Services.AddInfranstructure(builder.Configuration);


            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IEShopSeeder, EShopSeeder>();

            builder.Services.AddMemoryCache();


            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IEShopSeeder>();
            await seeder.Seed();

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
        }
    }

}

