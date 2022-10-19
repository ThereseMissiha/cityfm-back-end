using System.Text.Json.Serialization;

namespace Products.API.Models
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("maximumQuantity")]
        public int? MaximumQuantity { get; set; }


        [JsonPropertyName("salePrice")]
        public double SalePrice
        {
            get => UnitPrice + (UnitPrice * 0.2);
        }

    }
}
