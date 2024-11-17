using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ProductCode { get; set; }
    public required string Brand { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal ExTax { get; set; }
    public bool InStock { get; set; } = false;
    public int StockQuantity { get; set; }
    public int Rating { get; set; }
    public int RewardPoint { get; set; }
    public bool IsFeatured { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public List<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
