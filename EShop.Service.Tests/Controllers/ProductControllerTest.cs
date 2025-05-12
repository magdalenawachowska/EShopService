﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EShop.Application.Service;
using EShopService.Controllers;
using Microsoft.AspNetCore.Mvc;
using EShop.Domain.Models;

namespace EShop.Service.Tests.Controllers
{
    public class ProductControllerTest
    {

        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnAllProducts_ReturnTrue()
        {
            //Arrange
            var products = new List<Product> { new Product(), new Product() };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            //Act
            var result = await _controller.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsProduct_ReturnTrue()
        {
            //Arrange
            var product = new Product {  Id =1 };
            _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(product);

            //Act
            var result = await _controller.Get(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            //Arrange
            _mockService.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            //Act
            var result = await _controller.Get(600);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Post_ValidProduct_ReturnsCreateProduct()
        {
            //Arrange
            var newProduct = new Product();
            _mockService.Setup(s => s.Add(It.IsAny<Product>())).ReturnsAsync(newProduct);

            //Act
            var result = await _controller.Post(newProduct);

            //Assert
            _mockService.Verify(s => s.Add(newProduct), Times.Once);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task Put_ValidProduct_UpdatesAndReturnsOk()
        {
            // Arrange
            var product = new Product { Id = 1 };
            _mockService.Setup(s => s.UpdateAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _controller.Put(1, product);

            // Assert
            _mockService.Verify(s => s.UpdateAsync(product), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ValidId_MarksDeletedAndUpdates()
        {
            // Arrange
            var product = new Product { Id = 1, Deleted = false };
            _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(product);
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            _mockService.Verify(s => s.GetAsync(1), Times.Once);
            _mockService.Verify(s => s.UpdateAsync(It.Is<Product>(p => p.Deleted)), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }




    }
}
