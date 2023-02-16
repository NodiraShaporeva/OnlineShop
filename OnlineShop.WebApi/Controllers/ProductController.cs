using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("get_all")]
    public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAll(cancellationToken);
        return products;
    }

    [HttpGet("get_by_id")]
    public async Task<Product> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetById(id, cancellationToken);
        return product;
    }

    [HttpPost("add")]
    public async Task AddProduct(Product product, CancellationToken cancellationToken = default)
    {
        await _productRepository.Add(product, cancellationToken);
    }

    [HttpPut("update")]
    public async Task Edit(Product product, CancellationToken cancellationToken = default)
    {
        await _productRepository.Update(product, cancellationToken);
    }

    [HttpPost("delete")]
    public async Task DeleteProduct(Product product, CancellationToken cancellationToken = default)
    {
        await _productRepository.Delete(product, cancellationToken);
    }
}