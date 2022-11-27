#nullable enable
using System.Net.Http.Json;
using OnlineShop.Models;

namespace OnlineShop.HttpApiClient
{
    public class ShopClient: IShopClient
    {
        private const string DefaultHost = "https://localhost:7103";
        private readonly string _host;
        private readonly HttpClient _httpClient;

        public ShopClient(string host = DefaultHost, HttpClient? httpClient = null)
        {
            _host = host;
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken = default)
        {
            var uri = $"{_host}/get_all";
            var response = await _httpClient.GetFromJsonAsync<List<Product>>(uri, cancellationToken);
            return response!;
        }

        public async Task AddProduct(Product? product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            string uri = $"{_host}/add";
            var response = await _httpClient.PostAsJsonAsync(uri, product);
            response.EnsureSuccessStatusCode();
        }

        public async Task <Product?> GetProduct(Guid id)
        {
            string uri = $"{_host}/get_by_id?productId={id}";
            Product? product = await _httpClient.GetFromJsonAsync<Product>(uri);
            return product!;
        }

        public Task UpdateProduct(Product newProduct, Guid id)
        {
            if (newProduct is null)
            {
                throw new ArgumentNullException(nameof(newProduct));
            }

            string uri = $"{_host}/update?productId={id}";
            return _httpClient.PutAsJsonAsync(uri + id, newProduct);
        }

        public async Task DeleteProduct(Guid id)
        {
            string uri = $"{_host}/delete?productId={id}";
            var response = await _httpClient.DeleteAsync(uri + id);
            response.EnsureSuccessStatusCode();
        }
    }
}