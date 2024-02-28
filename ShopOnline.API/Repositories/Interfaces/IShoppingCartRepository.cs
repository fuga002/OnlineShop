using OnlineShop.Data.Entities;
using ShopOnline.Common.DTOs;

namespace ShopOnline.API.Repositories.Interfaces;

public interface IShoppingCartRepository
{
    Task<CartItem?> AddItem(CreateCartItemDto  item);
    Task<CartItem?> UpdateQty(int id, UpdateCartItemQtyDto item);
    Task<CartItem?> DeleteItem(int id);
    Task<CartItem?> GetItem(int id);
    Task<IEnumerable<CartItem>?> GetItems(int userId);
}