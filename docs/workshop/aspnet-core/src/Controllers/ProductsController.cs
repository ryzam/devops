using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Fetching all products from pod: {Hostname}", Environment.MachineName);
        var products = _productService.GetAll();
        return Ok(new
        {
            count = products.Count(),
            hostname = Environment.MachineName,
            products = products
        });
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation("Fetching product {ProductId} from pod: {Hostname}", id, Environment.MachineName);
        var product = _productService.GetById(id);
        
        if (product == null)
        {
            _logger.LogWarning("Product {ProductId} not found", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }

        return Ok(product);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest(new { message = "Product name is required" });
        }

        if (product.Price < 0)
        {
            return BadRequest(new { message = "Price cannot be negative" });
        }

        _logger.LogInformation("Creating new product: {ProductName} on pod: {Hostname}", 
            product.Name, Environment.MachineName);
        
        var created = _productService.Create(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(int id, [FromBody] Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest(new { message = "Product name is required" });
        }

        if (product.Price < 0)
        {
            return BadRequest(new { message = "Price cannot be negative" });
        }

        _logger.LogInformation("Updating product {ProductId} on pod: {Hostname}", id, Environment.MachineName);
        
        var updated = _productService.Update(id, product);
        if (updated == null)
        {
            _logger.LogWarning("Product {ProductId} not found for update", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }

        return Ok(updated);
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting product {ProductId} on pod: {Hostname}", id, Environment.MachineName);
        
        var deleted = _productService.Delete(id);
        if (!deleted)
        {
            _logger.LogWarning("Product {ProductId} not found for deletion", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }

        return NoContent();
    }
}