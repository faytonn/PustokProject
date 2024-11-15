using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class ProductImage : BaseEntity
{
    public required string ImageUrl { get; set; }
    public bool IsHover { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
