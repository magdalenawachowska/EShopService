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
using System.Text.Json;
using EShopService;
using EShop.Domain.Seeders;
using Newtonsoft.Json;


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

                        services.Remove(dbContextOptions);

                        // Stworzenie nowej bazy danych
                        services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MyDBForTest"));             
                         //.AddDbContext<DataContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductsDbTest;Trusted_Connection=True;"));

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
                await dbContext.Database.EnsureCreatedAsync();

                var allProducts = await dbContext.Products.IgnoreQueryFilters().ToListAsync();
                dbContext.Products.RemoveRange(allProducts);

                var allCategories = await dbContext.Set<Category>().ToListAsync();
                dbContext.RemoveRange(allCategories);

                await dbContext.SaveChangesAsync();

                //dbContext.Products.RemoveRange(dbContext.Products);

                var cat = new Category { Name = "Test" };
                dbContext.Categories.Add(cat);
                await dbContext.SaveChangesAsync();

                ////Stworzenie obiektu
                dbContext.Products.AddRange(

                 new Product { Name = "Product1ForTest", Ean = "111", Price = 25m, CategoryId = cat.Id },
                 new Product { Name = "Product2ForTest", Ean = "222", Price = 10m, CategoryId = cat.Id },
                 new Product { Name = "Product3ForTest", Ean = "333", Price = 5m, CategoryId = cat.Id }
                   
                );
                await dbContext.SaveChangesAsync();
                //var seeder = scope.ServiceProvider.GetRequiredService<IEShopSeeder>();
                //await seeder.Seed();
            }
            
            //Act
            var response = await _client.GetAsync("/api/product");

            //Assert
            response.EnsureSuccessStatusCode();
            // var responseData = response.Content.ReadAsStringAsync();
            //var productsList = JsonSerializer.Deserialize<List<Products>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            //Assert.NotEmpty(productsList);
            //Assert.Contains(productsList, p => p.Price == 25);

            Assert.Equal(3, products?.Count);       
        }

        [Fact]
        public async Task Post_Add_AddThousandsProducts_ExceptedThousandsProducts()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                // Pobranie kontekstu bazy danych
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                dbContext.Products.RemoveRange(dbContext.Products);

                for (int i = 0; i < 10000; i++)
                {
                    dbContext.Products.AddRange(
                        new Product { Name = "Product" + i }
                    );
                    // Zapisanie obiektu
                    dbContext.SaveChanges();
                }
            }

            // Act
            var response = await _client.GetAsync("/api/product");

            // Assert
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.Equal(10000, products?.Count);
        }

        [Fact]
        public async Task Post_Add_AddThousandsProductsAsync_ExceptedThousandsProducts()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                // Pobranie kontekstu bazy danych
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                dbContext.Products.RemoveRange(dbContext.Products);

                for (int i = 0; i < 10000; i++)
                {
                    dbContext.Products.AddRange(
                        new Product { Name = "Product" + i }
                    );
                    await dbContext.SaveChangesAsync();
                }
            }

            // Act
            var response = await _client.GetAsync("/api/product");

            // Assert
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.Equal(10000, products?.Count);
        }

        [Fact]
        public async Task Add_AddProduct_ExceptedOneProduct()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.SaveChanges();

                // Act
                var category = new Category
                {
                    Name = "test"
                };

                var product = new Product
                {
                    Name = "Product",
                    Category = category
                };

                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync("/api/Product", content);

                var result = await dbContext.Products.ToListAsync();

                // Assert
                Assert.Equal(1, result?.Count);
            }
        }
    }
}
