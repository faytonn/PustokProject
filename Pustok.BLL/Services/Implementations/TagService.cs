using AutoMapper;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;

namespace Pustok.BLL.Services.Implementations;

public class TagService : ITagService
{
    private readonly IRepository<Tag> _tagRepository;
    private readonly IMapper _mapper;

    public TagService(IRepository<Tag> tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<TagViewModel> CreateTagAsync(CreateTagViewModel model)
    {
        var tag = _mapper.Map<Tag>(model);
        var createdTag = await _tagRepository.CreateAsync(tag);

        return _mapper.Map<TagViewModel>(createdTag);
    }

    public async Task<TagViewModel> DeleteTagAsync(int id)
    {
        var tag = await _tagRepository.GetAsync(id);
        if (tag == null) throw new Exception("Tag not found.");

        var deletedTag = await _tagRepository.DeleteAsync(tag);
        return _mapper.Map<TagViewModel>(deletedTag);
    }

    public async Task<TagListViewModel> GetPaginatedTagsAsync(int pageNumber, int pageSize)
    {
        var tags = await _tagRepository.GetPagesAsync(index: pageNumber, size: pageSize);
        var tagViewModels = _mapper.Map<List<TagViewModel>>(tags.Items);

        return new TagListViewModel
        {
            Items = tagViewModels,
            Count = tags.Count,
            Index = pageNumber,
            Size = pageSize,
            Pages = tags.Pages
        };
    }

    public async Task<TagViewModel> GetTagByIdAsync(int id)
    {
        var tag = await _tagRepository.GetAsync(id);
        if (tag == null) throw new Exception("Tag not found.");

        return _mapper.Map<TagViewModel>(tag);
    }

    public async Task<List<SelectListItem>> GetTagsForDropdownAsync()
    {
        var tags = await _tagRepository.GetAllAsync(t => true);
        return tags.Select(t => new SelectListItem
        {
            Text = t.Name,
            Value = t.Id.ToString()
        }).ToList();
    }

    public async Task<TagViewModel> UpdateTagAsync(int id, UpdateTagViewModel model)
    {
        var existingTag = await _tagRepository.GetAsync(id);
        if (existingTag == null) throw new Exception("Tag not found.");

        _mapper.Map(model, existingTag);
        var updatedTag = await _tagRepository.UpdateAsync(existingTag);

        return _mapper.Map<TagViewModel>(updatedTag);
    }
}
