using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.Entities;

public class ProductCategory
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}