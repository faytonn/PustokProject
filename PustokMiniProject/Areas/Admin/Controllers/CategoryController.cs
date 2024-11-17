using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;

namespace PustokMiniProject.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Moderator")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }


    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var categories = await _categoryService.GetPaginatedCategoriesAsync(pageNumber, pageSize);

        return View(categories);
    }

    public async Task<IActionResult> Create()
    {
        var parentCategories = await _categoryService.GetParentCategoriesAsync();

        CreateCategoryViewModel vm = new()
        {
            ParentCategories = parentCategories
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.ParentCategories = await _categoryService.GetParentCategoriesAsync();
            return View(vm);
        }

        await _categoryService.CreateCategoryAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var parentCategories = await _categoryService.GetParentCategoriesAsync();
        var updateVm = _mapper.Map<UpdateCategoryViewModel>(category);
        updateVm.ParentCategories = parentCategories;

        return View(updateVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateCategoryViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var parentCategories = await _categoryService.GetParentCategoriesAsync();

            vm = new()
            {
                ParentCategories = parentCategories
            };

            return View(vm);
        }

        await _categoryService.UpdateCategoryAsync(vm.Id, vm);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, string deleteOption)
    {
        if (deleteOption == "deleteWithSubcategories")
        {
            await _categoryService.DeleteCategoryAsync(id);
        }
        else if (deleteOption == "removeAndOrphanChildCategories")
        {
            await _categoryService.RemoveAndOrphanChildCategories(id);
        }

        return RedirectToAction(nameof(Index));
    }
}
