﻿namespace Pustok.BLL.ViewModels;

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
    public bool IsFeatured { get; set; }

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
    public bool IsFeatured { get; set; }

    public List<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
    public List<int>? CategoryIds { get; set; }
    public IFormFile? MainImage { get; set; } 
    public string? MainImageUrl{ get; set; }
    public IFormFile? HoverImage { get; set; }
    public string? HoverImageUrl{ get; set; }  
    public List<IFormFile> AdditionalImages { get; set; } = new List<IFormFile>();
    public List<string> AdditionalImageUrls { get; set; } = new List<string>();
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
    public decimal? DiscountPercentage { get; set; }
    public decimal ExTax { get; set; }
    public bool InStock { get; set; } = false;
    public int StockQuantity { get; set; }
    public int Rating { get; set; }
    public int RewardPoint { get; set; }
    public bool IsFeatured { get; set; }
    public decimal DiscountPrice => DiscountPercentage != null
    ? OriginalPrice * (1 - DiscountPercentage.Value / 100)
    : OriginalPrice;


    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    public List<int>? CategoryIds { get; set; }
    public IFormFile? MainImage { get; set; }
    public string? MainImageUrl { get; set; }
    public IFormFile? HoverImage { get; set; }
    public string? HoverImageUrl { get; set; }
    public List<IFormFile> AdditionalImages { get; set; } = new List<IFormFile>();
    public List<string> AdditionalImageUrls { get; set; } = new List<string>();
    public List<SelectListItem> ProductTags { get; set; } = new List<SelectListItem>();
    public List<int> ProductTagIds { get; set; } = new List<int>();

}

public class ProductListViewModel : PageableViewModel, IViewModel
{
    public List<ProductViewModel> Items { get; set; } = new List<ProductViewModel>();
}