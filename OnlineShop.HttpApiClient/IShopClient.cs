using OnlineShop.Models;

namespace OnlineShop.HttpApiClient;

public interface IShopClient
{
   Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken);
   Task AddProduct(Product? product);
   Task<Product?> GetProduct(Guid id);
   Task UpdateProduct(Product newProduct, Guid id);
   Task DeleteProduct(Guid id);
}