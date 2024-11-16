namespace Pustok.BLL.ViewModels;

public class ProductViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ProductCode { get; set; }
    public string? Brand { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal ExTax { get; set; }
    public bool InStock { get; set; } = false;
    public int StockQuantity { get; set; }
    public int Rating { get; set; }
    public int RewardPoint { get; set; }

    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
    public List<string> ProductTags { get; set; } = new List<string>();
}

public class CreateProductViewModel : IViewModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ProductCode { get; set; }
    public required string Brand { get; set;}
    public decimal OriginalPrice { get; set; }  
    public decimal DiscountPrice { get; set; }
    public decimal ExTax { get; set; }
    public bool InStock { get; set; }
    public int StockQuantity { get; set;}
    public int Rating { get; set; }
    public int RewardPoint { get; set; }

    public List<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
    public List<int>? CategoryIds { get; set; }
    public List<IFormFile>? Images { get; set; } = new List<IFormFile>();
    public List<SelectListItem>? ProductTags { get; set; }
    public List<int> ProductTagIds { get; set; } = new List<int>();
}

public class UpdateProductViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? ProductCode { get; set; }
    public string? Brand { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal ExTax { get; set; }
    public bool InStock { get; set; } = false;
    public int StockQuantity { get; set; }
    public int Rating { get; set; }
    public int RewardPoint { get; set; }

    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    public List<int>? CategoryIds { get; set; }
    public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    public List<SelectListItem> ProductTags { get; set; } = new List<SelectListItem>();
    public List<int> ProductTagIds { get; set; } = new List<int>();

}

public class ProductListViewModel : PageableViewModel, IViewModel
{
    public List<ProductViewModel> Items { get; set; } = new List<ProductViewModel>();
}