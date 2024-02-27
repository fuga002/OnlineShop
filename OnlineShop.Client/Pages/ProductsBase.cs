using Microsoft.AspNetCore.Components;
using OnlineShop.Client.Services.Interfaces;
using ShopOnline.Common.DTOs;

namespace OnlineShop.Client.Pages;

public class ProductsBase:ComponentBase
{
    [Inject]
    public IProductService ProductService { get; set; }

    protected IEnumerable<ProductDto>? Products { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = await ProductService.GetProducts();
    }

    protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
    {
        return from product in Products
            group product by product.CategoryId
            into prodByCatGroup
            orderby prodByCatGroup.Key
            select prodByCatGroup;
    }

    protected string? GetCategoryName(IGrouping<int, ProductDto> groupedProductDtOs)
    {
        return groupedProductDtOs.FirstOrDefault(gp => gp.CategoryId == groupedProductDtOs.Key)?.CategoryName;
    }
}