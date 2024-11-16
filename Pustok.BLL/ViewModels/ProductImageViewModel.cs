namespace Pustok.BLL.ViewModels;

public class ProductImageViewModel : IViewModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public bool IsHover { get; set; } = false;
    public bool IsSecondary { get; set; } = false;
}

public class CreateProductImageViewModel : IViewModel
{
    public required IFormFile ImageFile { get; set; }
    public string ImageUrl { get; set; }
    public bool IsHover { get; set; } = false;
    public bool IsSecondary { get; set; } = false;
}

public class UpdateProductImageViewModel : IViewModel
{
    public int Id { get; set; }
    public required IFormFile ImageFiles { get; set; }
    public string ImageUrl { get; set; }
    public bool IsHover { get; set; } = false;
    public bool IsSecondary { get; set; } = false;
}

public class ProductImageListViewModel : IViewModel
{
    public List<ProductImageViewModel> ProductImages { get; set; } = new List<ProductImageViewModel>();
}