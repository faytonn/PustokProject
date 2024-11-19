using AutoMapper;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;
using Pustok.BLL.Helpers.Utilities;
using CloudinaryDotNet;
using Pustok.BLL.Helpers.Abstractions;

namespace Pustok.BLL.Services.Implementations;

public class SliderService : ISliderService
{
    private readonly IRepository<Slider> _sliderRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public SliderService(IRepository<Slider> sliderRepository, IMapper mapper,ICloudinaryService cloudinaryService)
    {
        _sliderRepository = sliderRepository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
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

    public async Task<ResultViewModel<SliderViewModel>> CreateSliderAsync(CreateSliderViewModel model)
    {
        var imageInspection = Validate(model.SliderImage);
        if(imageInspection != null)
        {
            return imageInspection;
        }

        model.ImageUrl = await _cloudinaryService.ImageCreateAsync(model.SliderImage);

        var result = Validate(model.OriginalPrice);
        if(result != null)
        {
            return result;
        }

        var slider = _mapper.Map<Slider>(model);
        var createdSlider = await _sliderRepository.CreateAsync(slider);

        return _mapper.Map<ResultViewModel<SliderViewModel>>(createdSlider);
    }

    public async Task<ResultViewModel<SliderViewModel>> UpdateSliderAsync(int id, UpdateSliderViewModel model)
    {
        if (model.SliderImage != null)
        {
            var imageInspection = Validate(model.SliderImage);
            if(imageInspection != null)
            {
                return imageInspection;
            }
            _cloudinaryService.FileDeleteAsync(model.ImageUrl);
            model.ImageUrl = await _cloudinaryService.ImageCreateAsync(model.SliderImage);
        }

        var existingSlider = await _sliderRepository.GetAsync(id);
        if (existingSlider == null) throw new Exception("Slider not found.");

        _mapper.Map(model, existingSlider);
        var updatedSlider = await _sliderRepository.UpdateAsync(existingSlider);

        return _mapper.Map<ResultViewModel<SliderViewModel>>(updatedSlider);
    }

    public async Task<SliderViewModel> DeleteSliderAsync(int id)
    {
        var slider = await _sliderRepository.GetAsync(id);
        if (slider == null) throw new Exception("Slider not found.");

        _cloudinaryService.FileDeleteAsync(slider!.ImageUrl!);

        var deletedSlider = await _sliderRepository.DeleteAsync(slider);
        return _mapper.Map<SliderViewModel>(deletedSlider);
    }

    private ResultViewModel<SliderViewModel> Validate(IFormFile value)
    {
        if (!value.CheckType())
        {
            return new ResultViewModel<SliderViewModel>
            {
                Success = false,
                Message = "The file should be an image."
            };
        }

        return null;
    }

    private ResultViewModel<SliderViewModel> Validate(decimal value)
    {
        if(value < 0)
        {
            return new ResultViewModel<SliderViewModel>
            {
                Success = false,
                Message = "Value cannot be a negative number."
            };
        }

        return null;
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        var imageUrl = await _cloudinaryService.FileCreateAsync(image);
        return imageUrl;
    }
}
