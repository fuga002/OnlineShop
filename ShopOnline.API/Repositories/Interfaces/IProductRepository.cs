using OnlineShop.Data.Entities;

namespace ShopOnline.API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>?> GetProducts();
    Task<IEnumerable<ProductCategory>?> GetCategories();
    Task<Product?> GetProduct(int id);
    Task<ProductCategory?> GetCategory(int id);

}