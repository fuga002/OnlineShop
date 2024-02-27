using Microsoft.AspNetCore.Components;
using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Pages;

public class DisplayProductsBase:ComponentBase
{
    [Parameter]
    public IEnumerable<ProductDto> Products { get; set; } 
}