using OnlineShop.Models;

namespace OnlineShop.HttpApiClient;

public interface IShopClient
{
   Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken = default);
   Task AddProduct(Product product, CancellationToken cancellationToken = default);
   Task<Product?> GetProduct(Guid id, CancellationToken cancellationToken = default);
   Task UpdateProduct(Product newProduct, Guid id, CancellationToken cancellationToken = default);
   Task DeleteProduct(Guid id, CancellationToken cancellationToken = default);
}