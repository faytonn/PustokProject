namespace Pustok.BLL.ViewModels;

public class CategoryViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsHover { get; set; }
    public int? ParentCategoryId { get; set; }
    public CategoryViewModel? ParentCategory { get; set; }
    public List<CategoryViewModel> ChildrenCategories { get; set; } = new List<CategoryViewModel>();
}

public class CreateCategoryViewModel : IViewModel
{
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public List<SelectListItem> AvailableParents { get; set; } = new List<SelectListItem>();
}

public class UpdateCategoryViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public int? ParentId { get; set; }
    public List<SelectListItem> AvailableParents { get; set; } = new List<SelectListItem>();
}

public class CategoryListViewModel : IViewModel
{
    public List<CategoryViewModel> Items { get; set; } = new List<CategoryViewModel>();
}