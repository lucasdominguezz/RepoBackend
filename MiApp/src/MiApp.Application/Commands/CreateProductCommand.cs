using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;

namespace MiApp.Application.Commands;

public class CreateProductCommand
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommand(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> ExecuteAsync(string name, decimal price, int stock)
    {
        var product = new Product(name, price, stock);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return product;
    }
}
