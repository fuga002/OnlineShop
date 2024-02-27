using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto?> GetProduct(int id);
}