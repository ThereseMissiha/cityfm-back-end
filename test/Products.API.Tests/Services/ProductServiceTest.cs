using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Products.API.Clients;
using Products.API.Models;
using Products.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.API.Tests.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductList()
        {
            var mockClient = new Mock<IProductClient>();
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    ProductId = "1",
                    Name = "Name1",
                    Description = "Description1",
                    MaximumQuantity = 10,
                    UnitPrice = 100
                },
                new Product
                {
                    ProductId = "2",
                    Name = "Name2",
                    Description = "Description2",
                    MaximumQuantity = 10,
                    UnitPrice = 200
                }
            };
            mockClient.Setup(x => x.GetProductList()).ReturnsAsync(expectedProducts);
            var productService = new ProductService(mockClient.Object);
            var products = await productService.GetProducts();
            expectedProducts.Should().BeEquivalentTo(products);
            mockClient.Verify(x => x.GetProductList(), Times.Once);
        }
    }
}
