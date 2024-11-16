namespace Pustok.BLL.ViewModels;

public class TagViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<ProductTagViewModel> ProductTags { get; set; } = new List<ProductTagViewModel>();
}

public class CreateTagViewModel : IViewModel
{
    public string? Name { get; set; }
}

public class UpdateTagViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class TagListViewModel : PageableViewModel, IViewModel
{
    public List<TagViewModel> Items { get; set; } = new List<TagViewModel>();
}