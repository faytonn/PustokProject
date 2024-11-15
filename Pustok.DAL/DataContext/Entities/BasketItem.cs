using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class BasketItem : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public AppUser? User { get; set; }
    public required string UserId { get; set; }
}
