using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Services.Abstractions;

public interface ISliderService
{
    Task<SliderListViewModel> GetPaginatedSlidersAsync(int pageNumber, int pageSize);
    Task<SliderViewModel> GetSliderByIdAsync(int id);
    Task<ResultViewModel<SliderViewModel>> CreateSliderAsync(CreateSliderViewModel model);
    Task<ResultViewModel<SliderViewModel>> UpdateSliderAsync(int id, UpdateSliderViewModel model);
    Task<SliderViewModel> DeleteSliderAsync(int id);
    Task<string> UploadImageAsync(IFormFile image);
}
