using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Services.Abstractions
{
    public interface ITagService
    {
        Task<TagListViewModel> GetPaginatedTagsAsync(int pageNumber, int pageSize);
        Task<TagViewModel> GetTagByIdAsync(int id);
        Task<TagViewModel> CreateTagAsync(CreateTagViewModel model);
        Task<TagViewModel> UpdateTagAsync(int id, UpdateTagViewModel model);
        Task<TagViewModel> DeleteTagAsync(int id);
        Task<List<SelectListItem>> GetTagsForDropdownAsync();
    }
}
