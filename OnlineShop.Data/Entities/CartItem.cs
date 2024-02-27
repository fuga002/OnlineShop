using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.Entities;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
}