using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Services.Abstractions;

public interface ISliderService
{
    Task<SliderListViewModel> GetPaginatedSlidersAsync(int pageNumber, int pageSize);
    Task<SliderViewModel> GetSliderByIdAsync(int id);
    Task<SliderViewModel> CreateSliderAsync(CreateSliderViewModel model);
    Task<SliderViewModel> UpdateSliderAsync(int id, UpdateSliderViewModel model);
    Task<SliderViewModel> DeleteSliderAsync(int id);
}
