using OnlineShop.Domain.Entities;
using OnlineShop.HttpModels.Request;
using OnlineShop.HttpModels.Response;

namespace OnlineShop.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken = default);
    Task AddProduct(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetProduct(Guid id, CancellationToken cancellationToken = default);
    Task UpdateProduct(Product newProduct, Guid id, CancellationToken cancellationToken = default);
    Task DeleteProduct(Guid id, CancellationToken cancellationToken = default);
    Task<LogInResponse> Register(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<LogInResponse> LogIn(LogInRequest request, CancellationToken cancellationToken = default);
    void SetAuthToken(string token, CancellationToken cancellationToken = default);
    Task<Account> GetAccount(CancellationToken cancellationToken=default);
    Task<Cart?> GetCart(CancellationToken cancellationToken=default);
}