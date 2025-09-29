using EShop.Domain.Models;
using EShop.Domain.Seeders;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShopService.IntegrationTests
{
    public class CreditCardControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public CreditCardControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Usuń oryginalny DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    //services.AddDbContext<DataContext>(options =>
                    //    options.UseInMemoryDatabase("CreditCardTestDb"));
                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("CreditCardTestDb"));
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_ValidCardNumber_ReturnsCardProvider()
        {
            // Arrange 
            var cardNumber = "4532208021504434"; // Visa

            // Act
            var response = await _client.GetAsync($"/api/creditcard?cardNumber={cardNumber}");

            // Assert
            response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadFromJsonAsync<dynamic>();
            //Assert.Equal("Visa", (string)content.cardProvider);


            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.True(json.TryGetProperty("cardProvider", out var cp),
                $"Unexpected payload: {await response.Content.ReadAsStringAsync()}");

            Assert.Equal("Visa", cp.GetString());
        }

        [Fact]
        public async Task Get_InvalidCardNumber_ReturnsBadRequest()
        {
            // Arrange
            var cardNumber = "1234ABC567890";

            // Act
            var response = await _client.GetAsync($"/api/creditcard?cardNumber={cardNumber}");

            // Assert Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); var content = await response.Content.ReadFromJsonAsync<dynamic>(); Assert.Contains("Invalid card number", (string)content.error);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains("Invalid card number", body, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Get_NotRecognizedCardProvider_ReturnsNotAcceptable()
        {
            // Arrange
            var cardNumber = "3528145728933714"; // nieobsługiwany

            // Act
            var response = await _client.GetAsync($"/api/creditcard?cardNumber={cardNumber}");

            //// Assert
            //Assert.Equal((HttpStatusCode)406, response.StatusCode);
            //var content = await response.Content.ReadFromJsonAsync<dynamic>();
            //Assert.Equal("Not recognized card provider", (string)content.error);
            Assert.Equal((HttpStatusCode)406, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains("Not recognized card provider", body, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Post_ValidCard_ReturnsOk()
        {
            // Arrange
            var card = new Card { CardNumber = "4532208021504434" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/creditcard", card);

            // Assert
            response.EnsureSuccessStatusCode();
            var returnedCard = await response.Content.ReadFromJsonAsync<Card>();
            Assert.Equal(card.CardNumber, returnedCard.CardNumber);
        }

        [Fact]
        public async Task Put_UpdateCard_ReturnsOk()
        {
            // Arrange
            var card = new Card { CardNumber = "4532208021504434" };

            // Act
            var response = await _client.PutAsJsonAsync("/api/creditcard/1", card);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Updated", content);
        }

        [Fact]
        public async Task Delete_RemoveCard_ReturnsOk()
        {
            // Act
            var response = await _client.DeleteAsync("/api/creditcard/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Removed", content);
        }
    }
}