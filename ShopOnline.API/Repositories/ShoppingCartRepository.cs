using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Contexts;
using OnlineShop.Data.Entities;
using ShopOnline.API.Repositories.Interfaces;
using ShopOnline.Common.DTOs;

namespace ShopOnline.API.Repositories;

public class ShoppingCartRepository: IShoppingCartRepository
{
    private readonly ShopDbContext _context;

    public ShoppingCartRepository(ShopDbContext context)
    {
        _context = context;
    }

    private async Task<bool> CartItemExists(int cartId, int productId)
    {
        return await _context.CartItems.AnyAsync(c => c.CartId == cartId 
                                                      && c.ProductId == productId);

    }

    public async Task<CartItem?> AddItem(CreateCartItemDto item)
    {
        if (await CartItemExists(item.CartId, item.ProductId) == false)
        {
            var model = await (from product in _context.Products
                where product.Id == item.ProductId
                select new CartItem()
                {
                    CartId = item.CartId,
                    ProductId = product.Id,
                    Qty = item.Qty
                }).SingleOrDefaultAsync();

            if (model != null)
            {
                var result = await _context.CartItems.AddAsync(model);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }
        return null;
    }

    public async Task<CartItem?> UpdateQty(int id, UpdateCartItemQtyDto model)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item != null)
        {
            item.Qty = model.Qty;
            await _context.SaveChangesAsync();
            return item;
        }

        return null;
    }

    public async Task<CartItem?> DeleteItem(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        return item;
    }

    public async Task<CartItem?> GetItem(int id)
    {
        return await(from cart in _context.Carts
            join cartItem in _context.CartItems
                on cart.Id equals cartItem.CartId
            where cartItem.Id == id
            select new CartItem()
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Qty = cartItem.Qty,
                CartId = cartItem.CartId,
            }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CartItem>?> GetItems(int userId)
    {
        return await (from cart in _context.Carts
            join cartItem in _context.CartItems 
                on cart.Id equals cartItem.CartId 
                where cart.UserId == userId
                    select new CartItem()
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Qty = cartItem.Qty,
                CartId = cartItem.CartId,
            }).ToListAsync();
    }
}