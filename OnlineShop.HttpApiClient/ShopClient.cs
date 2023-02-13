using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using OnlineShop.Domain.Entities;
using OnlineShop.HttpModels.Request;
using OnlineShop.HttpModels.Response;

namespace OnlineShop.HttpApiClient
{
    public class ShopClient : IShopClient
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
            var uri = $"{_host}/products/get_all";
            var response = await _httpClient.GetFromJsonAsync<List<Product>>(uri, cancellationToken);
            return response!;
        }

        public async Task AddProduct(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            string uri = $"{_host}/add";
            var response = await _httpClient.PostAsJsonAsync(uri, product, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Product?> GetProduct(Guid id, CancellationToken cancellationToken = default)
        {
            string uri = $"{_host}/products/get_by_id?id={id}";
            Product? product = await _httpClient.GetFromJsonAsync<Product>(uri, cancellationToken);
            return product!;
        }

        public Task UpdateProduct(Product newProduct, Guid id, CancellationToken cancellationToken)
        {
            if (newProduct == null) throw new ArgumentNullException(nameof(newProduct));
            string uri = $"{_host}/update?productId={id}";
            return _httpClient.PutAsJsonAsync(uri + id, newProduct, cancellationToken);
        }

        public async Task DeleteProduct(Guid id, CancellationToken cancellationToken = default)
        {
            string uri = $"{_host}/delete?productId={id}";
            var response = await _httpClient.DeleteAsync(uri + id, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<LogInResponse> Register(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            string uri = $"{_host}/accounts/register";
            var responseMessage = await _httpClient.PostAsJsonAsync(uri, request, cancellationToken);
            responseMessage.EnsureSuccessStatusCode();
            
            var response = await responseMessage.Content.ReadFromJsonAsync<LogInResponse>(cancellationToken: cancellationToken);
            SetAuthToken(response?.Token!);
            return response!;
        }

        public async Task<LogInResponse> LogIn(LogInRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            string uri = $"{_host}/accounts/login";
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(uri, request, cancellationToken);
            
            if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var json = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception(json);
            }
            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadFromJsonAsync<LogInResponse>(cancellationToken: cancellationToken);
            SetAuthToken(response?.Token!);
            return response!;
        }

        public void SetAuthToken(string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            var header = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = header;
        }

        public Task<Account> GetAccount(CancellationToken cancellationToken=default)
        {
            return _httpClient.GetFromJsonAsync<Account>($"{_host}/accounts/get_account", cancellationToken: cancellationToken)!;
        }
    }
}