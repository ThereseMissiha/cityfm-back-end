using Products.API.Models;

namespace Products.API.Clients
{
    public interface IProductClient
    {
        Task<IEnumerable<Product>?> GetProductList();
    }
}