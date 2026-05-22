using MiApp.Application.Interfaces;

namespace MiApp.Application.Commands;

public class DeleteProductCommand
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommand(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();

        return true;
    }
}
