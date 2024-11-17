//namespace Pustok.BLL.ViewModels;

//public class BasketItemViewModel : IViewModel
//{
//    public int ProductId { get; set; }
//    public string? ProductName { get; set; }
//    public string? ImageUrl { get; set; }
//    public decimal Price => DiscountPrice > 0 ? DiscountPrice : OriginalPrice;
//    public decimal OriginalPrice { get; set; }
//    public decimal DiscountPrice { get; set; }
//    public int Quantity { get; set; }

//    public decimal TotalPrice => Price * Quantity;
//}

//public class CreateBasketItemViewModel : IViewModel
//{
//    public int ProductId {  get; set; }
//    public int Quantity { get; set; }
//}

//public class UpdateBasketItemViewModel : IViewModel
//{
//    public int ProductId { get; set; }
//    public int Quantity { get; set; }
//}