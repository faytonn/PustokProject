using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Tag : BaseEntity
{
    public required string Name { get; set; }
    public List<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
