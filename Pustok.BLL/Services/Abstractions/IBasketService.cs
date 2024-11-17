using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Services.Abstractions;

public interface IBasketService
{
    Task<BasketItemViewModel> AddToBasketAsync(int productId, bool useDatabase);
    Task<BasketItemViewModel> IncreaseBasketItemAsync(int productId, bool useDatabase);
    Task<BasketItemViewModel> DecreaseBasketItemAsync(int productId, bool useDatabase);

    Task<List<BasketItemViewModel>> GetBasketItemsAsync(bool useDatabase);
    Task RemoveFromBasketAsync(int productId, bool useDatabase);
}
