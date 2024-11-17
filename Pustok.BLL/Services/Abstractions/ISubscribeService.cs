using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Services.Abstractions;

public interface ISubscribeService
{
    Task<SubscribeListViewModel> GetPaginatedSubscribesAsync(int pageNumber, int pageSize);
    Task<SubscribeViewModel> GetSubscribeByIdAsync(int id);
    Task<SubscribeViewModel> CreateSubscribeAsync(CreateSubscribeViewModel model);
    Task<SubscribeViewModel> UpdateSubscribeAsync(int id, UpdateSubscribeViewModel model);
    Task<SubscribeViewModel> DeleteSubscribeAsync(int id);
}
