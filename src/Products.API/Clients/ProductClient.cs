using Products.API.Extensions;
using Products.API.Models;
using System.Xml.Linq;

namespace Products.API.Clients
{
    public class ProductClient : IProductClient
    {
        readonly HttpClient _client;
        readonly ILogger<ProductClient> _logger;

        public ProductClient(HttpClient client, ILogger<ProductClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>?> GetProductList()
        {
            var response = await _client.GetAsync("");
            response.EnsureSuccessStatusCode();
            var products = await response.Deserialize<List<Product>>();
            return products;
        }
    }
}
