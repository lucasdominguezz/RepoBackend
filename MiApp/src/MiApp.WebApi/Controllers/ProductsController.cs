using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Commands;
using MiApp.Application.Queries;
using MiApp.Domain.Exceptions;
using MiApp.WebApi.Requests;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly GetProductsQuery _getProductsQuery;
    private readonly GetProductByIdQuery _getProductByIdQuery;
    private readonly CreateProductCommand _createProductCommand;
    private readonly UpdateProductCommand _updateProductCommand;
    private readonly DeleteProductCommand _deleteProductCommand;

    public ProductsController(
        GetProductsQuery getProductsQuery,
        GetProductByIdQuery getProductByIdQuery,
        CreateProductCommand createProductCommand,
        UpdateProductCommand updateProductCommand,
        DeleteProductCommand deleteProductCommand)
    {
        _getProductsQuery = getProductsQuery;
        _getProductByIdQuery = getProductByIdQuery;
        _createProductCommand = createProductCommand;
        _updateProductCommand = updateProductCommand;
        _deleteProductCommand = deleteProductCommand;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _getProductsQuery.ExecuteAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _getProductByIdQuery.ExecuteAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(ProductRequest request)
    {
        try
        {
            var product = await _createProductCommand.ExecuteAsync(request.Name, request.Price, request.Stock);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductRequest request)
    {
        try
        {
            var product = await _updateProductCommand.ExecuteAsync(id, request.Name, request.Price, request.Stock);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _deleteProductCommand.ExecuteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
