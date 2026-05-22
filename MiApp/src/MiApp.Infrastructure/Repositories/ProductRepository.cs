using Microsoft.EntityFrameworkCore;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Product>> GetAllAsync()
    {
        return _context.Products.AsNoTracking().ToListAsync();
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return _context.Products.FindAsync(id).AsTask();
    }

    public Task AddAsync(Product product)
    {
        return _context.Products.AddAsync(product).AsTask();
    }

    public Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
