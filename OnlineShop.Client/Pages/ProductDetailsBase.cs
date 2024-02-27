using Microsoft.AspNetCore.Components;
using OnlineShop.Client.Services.Interfaces;
using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Pages;

public class ProductDetailsBase:ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IProductService ProductService { get; set; }

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    protected ProductDto? Product { get; set; }

    protected string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Product = await ProductService.GetProduct(Id);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    protected async Task AddToCart_Click(CreateCartItemDto model)
    {
        try
        {
            var cartItemDto = await ShoppingCartService.AddItem(model);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}