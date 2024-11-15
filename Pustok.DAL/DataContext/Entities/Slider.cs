using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Slider : BaseEntity
{
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required string ImageUrl { get; set; }
    public decimal OriginalPrice { get; set; }
    public bool DiscountPrice { get; set; }
}
