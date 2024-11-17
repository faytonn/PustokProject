namespace Pustok.BLL.ViewModels;

public class BasketItemViewModel : IViewModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

}
