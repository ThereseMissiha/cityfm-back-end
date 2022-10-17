using Products.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.API.Tests.Models
{
    public class ProductTest
    {

        [Fact]
        public void SalePrice_ShouldBe_120_For_UnitPrice_100()
        {
            var product = new Product()
            {
                ProductId = "ProductId",
                Name = "Name",
                Description = "Description",
                MaximumQuantity = 10,
                UnitPrice = 100
            };
            Assert.Equal(120, product.SalePrice);
        }
    }
}
