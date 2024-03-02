using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OnlineShop.Client.Services.Interfaces;
using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Pages;

public class ShoppingCartBase:ComponentBase
{

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    protected List<CartItemDto>? ShoppingCartItems { get; set; }

    protected string? ErrorMessage { get; set; }

    protected string TotalPrice { get; set; }
    protected int TotalQuantity { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            CalculateCartSummaryTotals();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    protected async Task DeleteCartItem_Click(int id)
    {
        var cartItemDto = await ShoppingCartService.DeleteItem(id);

        RemoveCartItem(id);
        CalculateCartSummaryTotals();
    }

    private void UpdateTotalPrice(CartItemDto model)
    {
        var item = GetCartItem(model.Id);

        if (item != null)
        {
            item.TotalPrice = model.Price * model.Qty;
        }
    }

    private CartItemDto? GetCartItem(int id)
    {
        return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
    }

    private void RemoveCartItem(int id)
    {
        var cartItemDto = GetCartItem(id);
        ShoppingCartItems.Remove(cartItemDto);

    }

    protected async Task UpdateQtyCartItem_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var updateItemDto = new UpdateCartItemQtyDto()
                {
                    CartItemId = id,
                    Qty = qty
                };

                var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);
                UpdateTotalPrice(returnedUpdateItemDto);
                CalculateCartSummaryTotals();
                await MakeUpdateQtyButtonVisible(id, false);
            }
            else
            {
                var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    item.Qty = 1;
                    item.TotalPrice = item.Price;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected async Task UpdateQty_Input(int id)
    {
        await MakeUpdateQtyButtonVisible(id, true);
    }

    private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
    {
        await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
    }

    private void SetTotalPrice()
    {
        TotalPrice = ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
    }

    private void SetTotalQuantity()
    {
        TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);
    }

    private void CalculateCartSummaryTotals()
    {
        SetTotalPrice();
        SetTotalQuantity();
    }

}