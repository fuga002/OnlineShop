using Microsoft.AspNetCore.Components;
using OnlineShop.Client.Services.Interfaces;
using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Pages;

public class ShoppingCartBase:ComponentBase
{

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    protected IEnumerable<CartItemDto>? ShoppingCartItems { get; set; }

    protected string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    protected async Task DeleteCartItem_Click(int id)
    {
        var cartItemDto = await ShoppingCartService.DeleteItem(id);

    }

}