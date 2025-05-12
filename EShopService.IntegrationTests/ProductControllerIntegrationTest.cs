using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;



namespace EShopService.IntegrationTests
{
    public class ProductControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public ProductControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // pobranie dotychczasowej konfiguracji bazy danych
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                        //// usunięcie dotychczasowej konfiguracji bazy danych
                        services.Remove(dbContextOptions);

                        // Stworzenie nowej bazy danych
                        services
                         //   .AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MyDBForTest"));              //co z tym inmemory?
                         .AddDbContext<DataContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductsDbTest;Trusted_Connection=True;"));

                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_ReturnsAllProducts()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                dbContext.Products.RemoveRange(dbContext.Products);

                //Stworzenie obiektu
                dbContext.Products.AddRange(

                    new Product { Name = "Product1" },
                    new Product { Name = "Product2" });

                //zapisanie obiektu
                await dbContext.SaveChangesAsync();
            }

            //Act
            var response = await _client.GetAsync("/api/product");

            //Assert
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.Equal(2, products?.Count);
        }


    }
}
