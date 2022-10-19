using Products.API.Clients;
using Products.API.Models;

namespace Products.API.Services
{
    public class ProductService : IProductService
    {
        readonly IProductClient _productClient;

        public ProductService(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productClient.GetProductList();
            return products??new List<Product>();
        }
    }
}
