namespace ShopOnline.Common.DTOs;

public class CreateCartItemDto
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
}