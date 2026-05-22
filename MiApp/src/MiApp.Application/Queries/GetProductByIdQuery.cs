using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;

namespace MiApp.Application.Queries;

public class GetProductByIdQuery
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQuery(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Product?> ExecuteAsync(int id)
    {
        return _productRepository.GetByIdAsync(id);
    }
}
