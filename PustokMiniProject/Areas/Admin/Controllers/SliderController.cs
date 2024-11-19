using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.Services.Implementations;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;

namespace PustokMiniProject.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin, Moderator")]
public class SliderController : Controller
{
    IMapper _mapper;
    ISliderService _sliderService;
    public SliderController(IMapper mapper, ISliderService sliderService) 
    {
        _mapper = mapper;
        _sliderService = sliderService;
    }
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var sliders = _sliderService.GetPaginatedSlidersAsync(pageNumber, pageSize);

        return View(sliders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var slider = await _sliderService.GetSliderByIdAsync(id);
        if (slider == null)
        {
            return NotFound();
        }

        return View(slider);
    }

    public async Task<IActionResult> Create()
    {
        CreateSliderViewModel vm = new CreateSliderViewModel
        {
            Title = "Enter Title",
            Subtitle = "Enter Subtitle",
            ImageUrl = "image.url",
            OriginalPrice = 0,
            DiscountPrice = 0
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSliderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if(model.SliderImage != null)
        {
            model.ImageUrl = await _sliderService.UploadImageAsync(model.SliderImage);
        }

        await _sliderService.CreateSliderAsync(model);
        TempData["SuccessMessage"] = "Slider created successfully.";
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var slider = await _sliderService.GetSliderByIdAsync(id);
        if (slider == null) return NotFound();


        var vm = _mapper.Map<UpdateSliderViewModel>(slider);
        
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateSliderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingSlider = await _sliderService.GetSliderByIdAsync(id);
        if (existingSlider == null)
        {
            return NotFound();
        }

        if (model.SliderImage != null)
        {
            model.ImageUrl = await _sliderService.UploadImageAsync(model.SliderImage);
        }
        var updatedSlider = _mapper.Map(model, existingSlider);

        await _sliderService.UpdateSliderAsync(id, model);

        TempData["SuccessMessage"] = "Slider updated successfully.";

        return RedirectToAction(nameof(Index));
    }
}
