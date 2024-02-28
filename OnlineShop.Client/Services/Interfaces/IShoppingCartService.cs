﻿using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Services.Interfaces;

public interface IShoppingCartService
{
    Task<IEnumerable<CartItemDto>?> GetItems(int userId);
    Task<CartItemDto?> AddItem(CreateCartItemDto model);
    Task<CartItemDto?> DeleteItem(int id);
}