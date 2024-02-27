using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Contexts;
using OnlineShop.Data.Entities;
using ShopOnline.API.Repositories.Interfaces;

namespace ShopOnline.API.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ShopDbContext _context;

    public ProductRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>?> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<IEnumerable<ProductCategory>?> GetCategories()
    {
        var categories = await _context.ProductCategories.ToListAsync();
        return categories;
    }

    public async Task<Product?> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<ProductCategory?> GetCategory(int id)
    {
        var category = await _context.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
        return category;
    }
}