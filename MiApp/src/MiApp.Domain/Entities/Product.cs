using MiApp.Domain.Exceptions;

namespace MiApp.Domain.Entities;

public class Product
{
    private Product()
    {
        Name = string.Empty;
    }

    public Product(string name, decimal price, int stock)
    {
        Validate(name, price, stock);

        Name = name;
        Price = price;
        Stock = stock;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public void Update(string name, decimal price, int stock)
    {
        Validate(name, price, stock);

        Name = name;
        Price = price;
        Stock = stock;
    }

    private static void Validate(string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("El nombre del producto es obligatorio.");
        }

        if (price <= 0)
        {
            throw new DomainException("El precio debe ser mayor a cero.");
        }

        if (stock < 0)
        {
            throw new DomainException("El stock no puede ser negativo.");
        }
    }
}
