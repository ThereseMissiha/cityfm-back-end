using System.Text.Json;

namespace Products.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T?> Deserialize<T>(this HttpResponseMessage resp)
        {
            using var stream = await resp.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}
