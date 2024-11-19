namespace Pustok.BLL.ViewModels;

public class SliderViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? ImageUrl { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
}

public class CreateSliderViewModel : IViewModel
{
    public string? Title { set; get; }
    public string? Subtitle { set; get; }
    public IFormFile SliderImage { get; set; }
    public string? ImageUrl { set; get; }
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
}

public class UpdateSliderViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public IFormFile SliderImage {  set; get; }
    public string? ImageUrl { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
}

public class SliderListViewModel : PageableViewModel, IViewModel
{
    public List<SliderViewModel> Items = new List<SliderViewModel>();
}