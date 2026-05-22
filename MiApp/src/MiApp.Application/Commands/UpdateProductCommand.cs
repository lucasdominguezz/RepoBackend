using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;

namespace MiApp.Application.Commands;

public class UpdateProductCommand
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommand(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> ExecuteAsync(int id, string name, decimal price, int stock)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        product.Update(name, price, stock);
        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();

        return product;
    }
}
