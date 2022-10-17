using Microsoft.AspNetCore.Mvc.Testing;
using Products.API.Extensions;
using Products.API.Models;

namespace Products.API.Tests.Controllers
{
    public class ProductControllerTest
    {

        private HttpClient _httpClient;

        public ProductControllerTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetProducts_ReturnList()
        {
            var response = await _httpClient.GetAsync("/api/Products");
            response.EnsureSuccessStatusCode();
            var products = await response.Deserialize<List<Product>>();
            Assert.NotNull(products);
        }
    }
}
