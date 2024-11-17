using AutoMapper;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;

namespace Pustok.BLL.Services.Implementations;

public class SliderService : ISliderService
{
    private readonly IRepository<Slider> _sliderRepository;
    private readonly IMapper _mapper;

    public SliderService(IRepository<Slider> sliderRepository, IMapper mapper)
    {
        _sliderRepository = sliderRepository;
        _mapper = mapper;
    }

    public async Task<SliderListViewModel> GetPaginatedSlidersAsync(int pageNumber, int pageSize)
    {
        var sliders = await _sliderRepository.GetPagesAsync(index: pageNumber, size: pageSize);
        var sliderViewModels = _mapper.Map<List<SliderViewModel>>(sliders.Items);

        return new SliderListViewModel
        {
            Items = sliderViewModels,
            Count = sliders.Count,
            Index = pageNumber,
            Size = pageSize,
            Pages = sliders.Pages
        };
    }

    public async Task<SliderViewModel> GetSliderByIdAsync(int id)
    {
        var slider = await _sliderRepository.GetAsync(id);
        if (slider == null) throw new Exception("Slider not found.");

        return _mapper.Map<SliderViewModel>(slider);
    }

    public async Task<SliderViewModel> CreateSliderAsync(CreateSliderViewModel model)
    {
        var slider = _mapper.Map<Slider>(model);
        var createdSlider = await _sliderRepository.CreateAsync(slider);

        return _mapper.Map<SliderViewModel>(createdSlider);
    }

    public async Task<SliderViewModel> UpdateSliderAsync(int id, UpdateSliderViewModel model)
    {
        var existingSlider = await _sliderRepository.GetAsync(id);
        if (existingSlider == null) throw new Exception("Slider not found.");

        _mapper.Map(model, existingSlider);
        var updatedSlider = await _sliderRepository.UpdateAsync(existingSlider);

        return _mapper.Map<SliderViewModel>(updatedSlider);
    }

    public async Task<SliderViewModel> DeleteSliderAsync(int id)
    {
        var slider = await _sliderRepository.GetAsync(id);
        if (slider == null) throw new Exception("Slider not found.");

        var deletedSlider = await _sliderRepository.DeleteAsync(slider);
        return _mapper.Map<SliderViewModel>(deletedSlider);
    }
}
