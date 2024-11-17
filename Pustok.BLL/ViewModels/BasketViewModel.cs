namespace Pustok.BLL.ViewModels;

public class BasketViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
    public string ImagePath { get; set; } = null!;
}
