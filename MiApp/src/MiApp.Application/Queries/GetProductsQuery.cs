using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;

namespace MiApp.Application.Queries;

public class GetProductsQuery
{
    private readonly IProductRepository _productRepository;

    public GetProductsQuery(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<Product>> ExecuteAsync()
    {
        return _productRepository.GetAllAsync();
    }
}
