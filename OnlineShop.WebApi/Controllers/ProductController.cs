using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.WebApi.Data;

namespace OnlineShop.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> _productRepository;

    public ProductController(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("get_all")]
    public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken)
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
    public async Task AddProduct(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.Add(product, cancellationToken);
    }

    [HttpPut("update")]
    public async Task Edit(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.Update(product, cancellationToken);
    }

    [HttpPost("delete")]
    public async Task DeleteProduct(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(product, cancellationToken);
    }
}