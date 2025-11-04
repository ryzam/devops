using ProductApi.Models;

namespace ProductApi.Services;

public class ProductService
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public IEnumerable<Product> GetAll()
    {
        lock (_lock)
        {
            return _products.ToList();
        }
    }

    public Product? GetById(int id)
    {
        lock (_lock)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }

    public Product Create(Product product)
    {
        lock (_lock)
        {
            product.Id = _nextId++;
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _products.Add(product);
            return product;
        }
    }

    public Product? Update(int id, Product product)
    {
        lock (_lock)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return null;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Category = product.Category;
            existing.StockQuantity = product.StockQuantity;
            existing.UpdatedAt = DateTime.UtcNow;
            return existing;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;
            _products.Remove(product);
            return true;
        }
    }

    public void SeedData()
    {
        lock (_lock)
        {
            if (_products.Any()) return;

            var seedProducts = new List<Product>
            {
                new Product
                {
                    Id = _nextId++,
                    Name = "Laptop",
                    Description = "High-performance laptop for developers",
                    Price = 1299.99m,
                    Category = "Electronics",
                    StockQuantity = 50,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = _nextId++,
                    Name = "Mechanical Keyboard",
                    Description = "RGB mechanical keyboard with Cherry MX switches",
                    Price = 149.99m,
                    Category = "Accessories",
                    StockQuantity = 100,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = _nextId++,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with precision tracking",
                    Price = 59.99m,
                    Category = "Accessories",
                    StockQuantity = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = _nextId++,
                    Name = "4K Monitor",
                    Description = "27-inch 4K IPS monitor with HDR support",
                    Price = 599.99m,
                    Category = "Electronics",
                    StockQuantity = 30,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = _nextId++,
                    Name = "USB-C Hub",
                    Description = "Multi-port USB-C hub with HDMI and ethernet",
                    Price = 79.99m,
                    Category = "Accessories",
                    StockQuantity = 200,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _products.AddRange(seedProducts);
        }
    }
}