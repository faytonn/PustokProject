using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
    public int? ParentId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> ChildCategories { get; set;} = new List<Category>();
    public List<Product> Products { get; set; } = new List<Product>();
}
